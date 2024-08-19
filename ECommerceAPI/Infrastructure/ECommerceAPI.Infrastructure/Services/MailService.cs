using ECommerceAPI.Application.DTOs.Orders;
using ECommerceAPI.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "E-Commerce", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = int.Parse(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.Append("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.Append(_configuration["ReactClientUrl"]);
            mail.Append("/update-password/");
            mail.Append(userId);
            mail.Append("/");
            mail.Append(resetToken);
            mail.Append("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>E-Commerce");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, CompletedOrderDTO dto)
        {
            StringBuilder mail = new();
            mail.Append($"Merhaba {dto.UserName},<br><br>Siparişiniz başarıyla oluşturulmuştur.<br>Sipariş Detayları:<br><br>");
            mail.Append($"<table style=\"border-collapse: collapse; width: 100%;\">");
            mail.Append($"<thead>");
            mail.Append($"<tr style=\"background-color: #f2f2f2;\">");
            mail.Append($"<th style=\"text-align: left; padding: 8px;\">Ürün Resmi</th>");
            mail.Append($"<th style=\"text-align: left; padding: 8px;\">Ürün Adı</th>");
            mail.Append($"<th style=\"text-align: left; padding: 8px;\">Adet</th>");
            mail.Append($"<th style=\"text-align: left; padding: 8px;\">Fiyat</th>");
            mail.Append($"</tr>");
            mail.Append($"</thead>");
            mail.Append($"<tbody>");

            decimal totalPrice = 0;
            foreach ( var item in dto.BasketItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
                mail.Append($"<tr>");
                mail.Append($"<td style=\"text-align: left; padding: 8px;\"><img src=\"{item.Product.ImageUrl}\" alt=\"{item.Product.Name}\" width=\"50\" height=\"50\"></td>");
                mail.Append($"<td style=\"text-align: left; padding: 8px;\">{item.Product.Name}</td>");
                mail.Append($"<td style=\"text-align: left; padding: 8px;\">{item.Quantity}</td>");
                mail.Append($"<td style=\"text-align: left; padding: 8px;\">{item.Product.Price.ToString("N2")} TL</td>");
                mail.Append($"</tr>");
            }

            mail.Append($"</tbody>");
            mail.Append($"</table>");

            mail.Append($"<br><p><strong>Toplam Fiyat: {totalPrice.ToString("N2")} TL</strong></p><br>");

            mail.Append($"<br><br>Sipariş Kodu: #{dto.OrderCode}<br>");
            mail.Append($"Sipariş Tarihi: {dto.OrderDate}<br><br>");
            mail.Append($"Saygılarımızla...<br><br><br>E-Commerce");

            await SendMailAsync(to, "Siparişiniz Oluşturuldu", mail.ToString());
        }
    }
}
