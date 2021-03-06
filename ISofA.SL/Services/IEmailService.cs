﻿using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IEmailService
    {
        Task UserItemSoldNotification(UserItem userItem, Bid winningBid);
        Task SendMovieInvites(IEnumerable<ISofAUser> users, IEnumerable<int> projectionIds, IEnumerable<int> rows, IEnumerable<int> columns);
    }
}
