﻿using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService _userService)
        {
            this._userService = _userService;
        }
        [HttpGet]
        [Route("api/Users")]
        public IEnumerable<ISofAUserDTO> Get()
        {
            return _userService.GetNonFriendUsers(User.Identity.GetUserId());
        }
        [HttpGet]
        [Route("api/Users/myProfile")]
        public ISofAUserDTO GetMyProfile()
        {
            return _userService.GetUserProfile(User.Identity.GetUserId());
        }

        [HttpGet]
        [Route("api/Users/{userId}")] // TODO: pick a route api/{x:string} blocks every controller
        public ISofAUserDTO GetUserProfile(string userId)
        {
            return _userService.GetUserProfile(userId);
        }

        [HttpPost]
        [Route("api/Users/SearchAll")]
        public IEnumerable<ISofAUserDTO> GetUserFromSearch(ISofAUser user)
        {
            return _userService.GetUsers(User.Identity.GetUserId(), user.Name);
        }

        [HttpPost]
        [Route("api/Users/ChangeDetails")]
        public void ChangeDetails(ChangeDetailsBindingModel user)
        {
            _userService.ChangeUserDetails(User.Identity.GetUserId(), user.Name, user.Surname, user.City, user.PhoneNumber);
        }

	}
}
