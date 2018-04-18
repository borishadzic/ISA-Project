using ISofA.DAL.Core.Domain;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSection _smtpSection;

        public EmailService()
        {
            _smtpClient = new SmtpClient();
            _smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        }

        public async Task UserItemSoldNotification(UserItem userItem, Bid winningBid)
        {
            IEnumerable<String> emails = userItem.Bids
                .Where(x => x.BidderId != winningBid.BidderId)
                .Select(x => x.Bidder.Email)
                .ToList();

            foreach (var email in emails)
            {
                await _smtpClient.SendMailAsync(new MailMessage(_smtpSection.From, email)
                {
                    Subject = "ISofA FanZone",
                    Body = $"<p>Your bid for item <b>{userItem.Name}</b> has been declined.</p>",
                    IsBodyHtml = true
                });
            }

            await _smtpClient.SendMailAsync(new MailMessage(_smtpSection.From, winningBid.Bidder.Email)
            {
                Subject = "ISofA FanZone",
                Body = $"<p>Your bid for item <b>{userItem.Name}</b> has been selected as winning bid.</p>",
                IsBodyHtml = true
            });
        }
    }
}
