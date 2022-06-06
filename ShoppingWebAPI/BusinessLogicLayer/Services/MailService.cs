using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BusinessLogicLayer.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendOrderEmailAsync(OrderDto order)
        {
            // Get html template
            //string filePath ="/Users/dive95/Documents/Online-Shopping-site/ShoppingWebAPI/BusinessLogicLayer/Template/OrderDetails.html";
            //StreamReader str = new StreamReader(filePath);
            //string mailBody = str.ReadToEnd();
            //str.Close();

            // make list of order items
            //var orderItemsHtml = "";
            //foreach(var orderItem in order.OrderItems)
            //{
            //    orderItemsHtml += @$"
            //            <div class='col-8 d-flex align-items-center' style='display:flex; align-items:center'>
            //                <img src = '{orderItem.Image}'
            //                     style='width: 70px; margin-bottom: 2rem'
            //                     class='me-4' />
            //                <p>
            //                    {orderItem.Name},
            //                    <span class='fw-bold' style='font-weight:bold'>{orderItem.Size}</span>
            //                </p>
            //            </div>
            //            <div class='col-2 align-self-center' style='align-self:center'>
            //                <p style = 'color:gray;font-size: 15px' >
            //                    {orderItem.Quantity} Qty
            //                </p>
            //            </div>

            //            <div class='col-2 align-self-center' style='align-self:center'>
            //                <p style = 'font-size: 15px; > LKR {Convert.ToString(orderItem.Price)} </ p >
            //            </ div >";
            //}



            //StringBuilder mailBodyBuilder = new StringBuilder(mailBody);

            //mailBodyBuilder
            //    .Replace("[userName]", order.Shipping.FirstName + " " + order.Shipping.LastName)
            //    .Replace("[firstName]", order.Shipping.FirstName)
            //    .Replace("[firstName]", order.Shipping.LastName)
            //    .Replace("[orderStatus]", order.Shipping.Status)
            //    .Replace("[orderId]", Convert.ToString(order.Id))
            //    //.Replace("[subtotal]", Convert.ToString(order.Invoice.Total - order.Shipping.DeliveryOption.Price))
            //    //.Replace("[total]", Convert.ToString(order.Invoice.Total))
            //    //.Replace("[shippingPrice]", Convert.ToString(order.Shipping.DeliveryOption.Price))
            //    .Replace("[address]", order.Shipping.Address)
            //    .Replace("[paymentMethod]", order.Invoice.PaymentMethod)
            //    //.Replace("[phoneNum]", Convert.ToString(order.Shipping.PhoneNum))
            //    .Replace("[orderlist]", order.OrderItems.ToString())
            //    .Replace("[expectedDelivery]", order.Shipping.ExpectedDeliveryDate.ToLocalTime().ToString())
            //    .Replace("[orderDate]", order.OrderDate.ToLocalTime().ToString())
            //    .Replace("[orderItemList]", orderItemsHtml);

            //mailBody = mailBodyBuilder.ToString();



            var mailBody = @$"<!DOCTYPE html>
                <html>
                <head>
                    <meta charset = ""utf-8""/>
                     title></title>
                </head>
                <body>

                 <div style = ""max-width: 60vw; margin: 0 auto"" >
 
                    <h1 style = ""text-align:center; color:#333"" > Trends </ h1 >
                     <p style=""font-size: 16px""><span style = ""font-weight:bold""> Hi {order.Shipping.FirstName} , </span> your order is Confirmed !</ p >
             
                     <p style = ""font-size:16px"">
                          Thanks for shopping with us.You can see your summary of your recent purchase <a href = ""http://localhost:4200/order/{order.Id}"" > here </a>.
              
                          Please allow up to 3 business days(excluding weekends, holidays,
                          and sale days) to process and ship your order.You will receive another
                          email when your order has shipped.
                      </p>
                  </div>
                </body>
                </html>
              ";
              

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(order.Email));
            email.Subject = "Order Confirmation.";

            var builder = new BodyBuilder();
            builder.HtmlBody = mailBody;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
