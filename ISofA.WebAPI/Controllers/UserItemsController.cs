using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Filters;
using ISofA.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ISofA.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Theaters/{theaterId:int}/UserItems")]
    public class UserItemsController : ApiController
    {
        private readonly IUserItemService _userItemService;

        public UserItemsController(IUserItemService userItemService)
        {
            _userItemService = userItemService;
        }

        [Route("~/api/UserItems")]
        public IEnumerable<UserItemDTO> GetUserItems()
        {
            return _userItemService.GetUserItems(User.Identity.GetUserId());
        }

        [Route("")]
        public IEnumerable<UserItemDTO> Get(int theaterId)
        {
            return _userItemService.GetItemsForTheater(theaterId);
        }

        [Route("")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public IEnumerable<UserItemDTO> Get(int theaterId, [FromUri]string status)
        {
            if (status == "awaiting")
            {
                return _userItemService.GetAwaitingItemsForTheater(theaterId);
            }
            else if (status == "sold")
            {
                return _userItemService.GetSoldItems(theaterId);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
        }

        [Route("{userItemId:guid}")]
        public UserItemDetailDTO Get(int theaterId, Guid userItemId)
        {
            var userItem = _userItemService.GetItem(theaterId, userItemId);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return userItem;
        }

        [Route("")]
        public IHttpActionResult Post(int theaterId, UserItemBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userItem = new UserItem
            {
                Name = bindingModel.Name,
                Description = bindingModel.Description,
                ExpirationDate = bindingModel.ExpirationDate,
                ImageUrl = $"{ Url.Content("~/") }Images/default.png"
            };

            var addedItem = _userItemService.AddItem(theaterId, User.Identity.GetUserId(), userItem);

            if (addedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(addedItem);
        }

        [Route("{userItemId:guid}")]
        public async Task<UserItemDTO> PostImageAsync(int theaterId, Guid userItemId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var userItem = await _userItemService.SetImageAsync(User.Identity.GetUserId(), theaterId, userItemId, HttpContext.Current.Request.Files["image"]);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return userItem;
        }

        [Route("{userItemId:guid}")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public UserItemDTO Put(int theaterId, Guid userItemId)
        {
            var updateItem = _userItemService.ApproveItem(theaterId, userItemId);

            if (updateItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return updateItem;
        }

        [Route("{userItemId:guid}")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public void Delete(int theaterId, Guid userItemId)
        {
            _userItemService.RemoveItem(theaterId, userItemId);
        }
    }
}
