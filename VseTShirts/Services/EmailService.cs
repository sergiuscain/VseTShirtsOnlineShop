using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace VseTShirts.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort; // Используйте 465, если хотите использовать SSL
        private readonly string _smtpUser; // Ваш email
        private readonly string _smtpPass; // Пароль приложения

        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass) //настройки сервиса почты находятся в appsettings.json Там указываются все параметры, что есть в конструкторе
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.EnableSsl = true; // Включите SSL
                smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass); // Убедитесь, что аутентификация настроена

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при отправке email: {ex.Message}");
                    return false;
                }
            }
        }
    }
}