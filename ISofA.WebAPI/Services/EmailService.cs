using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.WebAPI.Services
{
    public class EmailService : IIdentityMessageService
    {
        private SmtpClient client1 = new SmtpClient();
        private SmtpSection sec1 = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        public async Task SendAsync(IdentityMessage message)
        {
            MailMessage mm = new MailMessage(sec1.From, message.Destination, message.Subject, message.Body)
            {
                IsBodyHtml =true
            };

            await client1.SendMailAsync(mm);
            
        }
    }
}