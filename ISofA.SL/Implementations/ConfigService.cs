using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class ConfigService : Service, IConfigService
    {
        public ConfigService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserLevelDTO GetUserLevel()
        {
            var bronze = UnitOfWork.Configs
                .Find(x => x.Key == ((int)UserLevel.Bronze).ToString())
                .FirstOrDefault();

            var silver = UnitOfWork.Configs
                .Find(x => x.Key == ((int)UserLevel.Silver).ToString())
                .FirstOrDefault();

            var gold = UnitOfWork.Configs
                .Find(x => x.Key == ((int)UserLevel.Gold).ToString())
                .FirstOrDefault();

            return new UserLevelDTO
            {
                BronzeLevel = int.Parse(bronze?.Value ?? "0"),
                SilverLevel = int.Parse(silver?.Value ?? "0"),
                GoldLevel = int.Parse(gold?.Value ?? "0")
            };
        }

        public UserLevelDTO AddUserLevel(UserLevelDTO userLevel)
        {
            if (userLevel.BronzeLevel >= userLevel.SilverLevel || userLevel.SilverLevel >= userLevel.GoldLevel)
            {
                return null;
            }

            Config bronze = new Config
            {
                Key = ((int)UserLevel.Bronze).ToString(),
                Value = userLevel.BronzeLevel.ToString()
            };

            Config silver = new Config
            {
                Key = ((int)UserLevel.Silver).ToString(),
                Value = userLevel.SilverLevel.ToString()
            };

            Config gold = new Config
            {
                Key = ((int)UserLevel.Gold).ToString(),
                Value = userLevel.GoldLevel.ToString()
            };

            UnitOfWork.Configs.SaveOrUpdate(bronze);
            UnitOfWork.Configs.SaveOrUpdate(silver);
            UnitOfWork.Configs.SaveOrUpdate(gold);
            UnitOfWork.SaveChanges();

            return userLevel;
        }
    }
}
