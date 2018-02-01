using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;        
        }

        public IEnumerable<ISofAUser> Get()
        {
            return _userRepository.Get();
        }
    }
}
