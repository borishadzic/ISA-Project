using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.DAL.Persistence.Pantries;
using ISofA.WebAPI.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class TheatersController : ApiController
    {
        private readonly ITheaterPantry _theaterPantry;

        public TheatersController(ITheaterPantry theaterPantry)
        {
            _theaterPantry = theaterPantry;
        }
    
        public IEnumerable<Theater> GetAll()
        {
            return _theaterPantry.GetAll();
        }

        [FanZoneAdmin]
        public Theater Get(int id)
        {
            return _theaterPantry.Get(id);
        }

        public Theater Post(Theater theater)
        {
            return _theaterPantry.Add(theater);
        }
    }
}
