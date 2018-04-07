using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserPantry _userPantry;

        public UsersController(IUserPantry userPantry)
        {
            _userPantry = userPantry;
        }
        
        public IEnumerable<ISofAUser> Get()
        {
            return _userPantry.GetAll();
        }

        public ISofAUser Get(string userId)
        {
            return _userPantry.Get(userId);
        }

    }
}
