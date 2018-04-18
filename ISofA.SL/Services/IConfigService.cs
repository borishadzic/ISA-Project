using ISofA.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IConfigService
    {
        UserLevelDTO GetUserLevel();
        UserLevelDTO AddUserLevel(UserLevelDTO userLevel);
    }
}
