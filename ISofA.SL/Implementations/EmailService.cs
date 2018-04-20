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

        public async Task SendMovieInvites(IEnumerable<ISofAUser> users, IEnumerable<int> projectionIds, IEnumerable<int> rows, IEnumerable<int> columns)
        {
            IEnumerable<string> emails = users.Select(x => x.Email).ToList();
            int i = 0;
            foreach(string email in emails)
            {
                int projectionId = projectionIds.ElementAt(i);
                int row = rows.ElementAt(i);
                int column = columns.ElementAt(i);
                string userId = users.ElementAt(i).Id;
                await _smtpClient.SendMailAsync(new MailMessage(_smtpSection.From, email)
                {
                    Subject = "Movie/Cinema Invite",
                    Body = $"<p>I invite you to go to the movie/cinema with me. <a href='http://localhost:49459/api/FriendReservations/ConfirmInvite/{userId}/{projectionId}/{row}/{column}'>Accept</a> of <a href='http://localhost:49459/api/FriendReservations/DeclineInvite/{projectionId}/{row}/{column}'>Decline</a></p>",
                    IsBodyHtml = true
                });
                i++;
            }
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
