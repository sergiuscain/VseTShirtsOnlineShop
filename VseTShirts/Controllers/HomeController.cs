using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Helpers;
using VseTShirts.Models;
using VseTShirts.Services;

namespace VseTShirts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsStorage _productStorage;
        public readonly IComparedProductsStorage _comparedStorage;
        private readonly UserManager<User> _userManager;
        private readonly ICollectionsStorage _collectionsStorage;
        private readonly IEmailService _emailService;

        public HomeController(IProductsStorage productStorage, IComparedProductsStorage comparedStorage, UserManager<User> userManager, ICollectionsStorage collectionsStorage, IEmailService emailService)
        {
            _productStorage = productStorage;
            _comparedStorage = comparedStorage;
            _userManager = userManager;
            _collectionsStorage = collectionsStorage;
            _emailService = emailService;
        }


        public async Task<IActionResult> Index(int? page)
        {
            page = page ?? 1;
            int pageCount = await _productStorage.GetPageCount(Data.pageSize);
            var filters = new FiltersViewModel
            {
                Category = "ALL",
                StartPrice = 0,
                EndPrice = 0,
                SortBy = "Price",
                Color = "ALL",
                Size = "ALL",
                Sex = "ALL",
                MinQuantity = 0,
                MaxQuantity = 0,
            };
            var productsViewModel = (await _productStorage.GetAllAsync(page, Data.pageSize)).ToViewModel();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexModel = new HomeIndexViewModel { Products = productsViewModel, Filters = filters, CollectionsList = collections, IsActiveFilters = false, PageCount = pageCount, Page = (int)page };
            return View(homeIndexModel);
        }

        public IActionResult Privacy(string a)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task<IActionResult> CompareAsync(Guid Id)
        {
            var product1 = await _productStorage.GetByIdAsync(Id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (await _comparedStorage.AddAsync(user.Id, product1))
            {
                var products = await _comparedStorage.GetByUserIdAsync(user.Id);
                if (products.Count < 2)
                    return RedirectToAction("Index");
                else
                    return View(products.ToViewModel());
            }
            return View((await _comparedStorage.GetByUserIdAsync(user.Id)).ToViewModel());
        }

        public async Task<IActionResult> RemoveCompareAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _comparedStorage.DeleteAsync(user.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchAsync(string serachTxt, int? page)
        {
            page = page ?? 1;
            var products = await _productStorage.GetAllAsync();
            if (!string.IsNullOrEmpty(serachTxt)) 
                products = products.Where(p => p.Name.ToLower().Contains(serachTxt.ToLower())).ToList();
            int pageCount = _productStorage.calculatePageCount(Data.pageSize, products.Count);
            products = products.Skip((int)(page - 1) * Data.pageSize)
                .Take(Data.pageSize).ToList();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexViewModel = new HomeIndexViewModel { Products = products.ToViewModel(), CollectionsList = collections, Page = (int)page, PageCount = pageCount, SearchString = serachTxt };
            return View("Index", homeIndexViewModel);
        }
        public async Task<IActionResult> FilterAsync(FiltersViewModel filters)
        {
            var products = await _productStorage.GetAllAsync();        
            var filtredProducts = await _productStorage.FiltrAsync(products, filters.ToDBModel());
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexViewModel = new HomeIndexViewModel { Products = filtredProducts.ToViewModel(), Filters = filters, CollectionsList = collections };
            return View("Index", homeIndexViewModel);
        }
        public async Task<IActionResult> CollectionAsync(string name)
        {
            var filters = new FiltersViewModel
            {
                Category = "ALL",
                StartPrice = 0,
                EndPrice = 0,
                SortBy = "Price",
                Color = "ALL",
                Size = "ALL",
                Sex = "ALL",
                MinQuantity = 0,
                MaxQuantity = 0,
            };
            var products = (await _productStorage.GetByCollectionAsync(name)).ToViewModel();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexModel = new HomeIndexViewModel { Products = products, Filters = filters, CollectionsList = collections, IsActiveFilters = false };
            return View(homeIndexModel);
        }

        public async Task<IActionResult> SednEmail()
        {
            string to = "test@yandex.ru"; //Кому отправляем
            string subject = "Это заголовок"; //Это заголовок письма
            //А снизу пример тела письма в виде html документа
            string htmlBodyMessage = "<!DOCTYPE html>\r\n<html lang=\"ru\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Тело письма</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n            margin: 0;\r\n            padding: 20px;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: auto;\r\n            background: #fff;\r\n            padding: 20px;\r\n            border-radius: 5px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            color: #333;\r\n        }\r\n        p {\r\n            color: #555;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            padding: 10px 20px;\r\n            background-color: #007BFF;\r\n            color: white;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n        }\r\n        .button:hover {\r\n            background-color: #0056b3;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Здравствуйте!</h1>\r\n        <p>Спасибо, что воспользовались нашим сервисом. Мы рады приветствовать вас!</p>\r\n        <p>Чтобы начать, нажмите на кнопку ниже:</p>\r\n        <a href=\"https://example.com\" class=\"button\">Начать</a>\r\n        <p>Если у вас есть вопросы, не стесняйтесь обращаться к нам.</p>\r\n        <p>С уважением,<br>Ваша команда</p>\r\n    </div>\r\n</body>\r\n</html>";
            await _emailService.SendEmailAsync(to,subject, htmlBodyMessage);
            return RedirectToAction("Index");
        }
    }
}
