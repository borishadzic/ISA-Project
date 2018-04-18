using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class ConfigPantry : Pantry<Config>, IConfigPantry
    {
        public ConfigPantry(ISofADbContext context) : base(context)
        {
        }

        public void SaveOrUpdate(Config config)
        {
            var foundConfig = Context.Configs.FirstOrDefault(x => x.Key == config.Key);

            if (foundConfig == null)
            {
                Context.Configs.Add(config);
            }
            else
            {
                foundConfig.Value = config.Value;
            }

            Context.SaveChanges();
        }
    }
}
