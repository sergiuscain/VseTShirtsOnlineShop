using VseTShirts.Models;
using VseTShirts.DB;
using Serilog;
using Microsoft.EntityFrameworkCore;
using VseTShirts.DB.Models;
using Microsoft.AspNetCore.Identity;
using VseTShirts.Helpers;
using VseTShirts.Services;
using System.Configuration;

namespace VseTShirts
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var emailSettings = builder.Configuration.GetSection("EmailSettings");
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            object value = builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer (connection));
            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));
            builder.Services.AddIdentity<User, IdentityRole>()
                           .AddEntityFrameworkStores<IdentityContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICollectionsStorage, CollectionsDBStorage>();
            builder.Services.AddTransient<ICartsStorage ,CartsDBStorage>();
            builder.Services.AddTransient<IProductsStorage ,ProductsDBStorage>();
            builder.Services.AddTransient<IFavoriteProductsStorage, FavoriteProductsDBStorage>();
            builder.Services.AddTransient<IOrdersStorage, OrdersDBStorage>();
            builder.Services.AddTransient<ImageProvider>();
            builder.Services.AddTransient<IComparedProductsStorage, ComparedProductsDBStorage>();
            builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("ApplicationName", "Online Shop"));
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IEmailService>(provider =>
                new EmailService(
                    emailSettings["SmtpServer"],
                    int.Parse(emailSettings["SmtpPort"]),
                    emailSettings["SmtpUser"],
                    emailSettings["SmtpPass"]
                ));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                 .AddCookie(options =>
                 {
                     options.ExpireTimeSpan = TimeSpan.FromHours(12);
                     options.LoginPath = "/Account/Login";
                     options.LogoutPath = "/Account/Logout";
                     options.Cookie = new CookieBuilder
                     {
                         IsEssential = true
                     };
                 })
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                })
                .AddYandex(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Yandex:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Yandex:ClientSecret"];
                });


            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                IdentityInitializer.Initialize(userManager, rolesManager);
            }

            app.UseSerilogRequestLogging();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
                }
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}