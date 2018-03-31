using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
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


        [Route("")]
        public IEnumerable<UserItemDTO> Get(int theaterId)
        {
            return _userItemService.GetItemsForTheater(theaterId);
        }

        // Administrator fan zone
        // api/theaters/1/useritem?status={ sold, awaiting }
        [Route("")]
        public IEnumerable<UserItemDTO> Get([FromUri]int theaterId, [FromUri]string status)
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

        [Route("~/api/UserItems/{userItemId:guid}")]
        [ResponseType(typeof(UserItemDetailDTO))]
        public IHttpActionResult Get(Guid userItemId)
        {
            var userItem = _userItemService.GetItem(userItemId);

            if (userItem == null)
            {
                return NotFound();
            }

            return Ok(userItemId);
        }

        [HttpPost]
        [Route("~/api/UserItems/{userItemId:guid}")]
        public async Task<UserItemDTO> UploadImageAsync(Guid userItemId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var userItem = await _userItemService.SetImageAsync(User.Identity.GetUserId(), userItemId, HttpContext.Current.Request.Files["image"]);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return userItem;
        }

        // Ulogovani korisnici
        [Route("")]
        [ResponseType(typeof(UserItemDTO))]
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
                return BadRequest();
            }

            return Ok(addedItem);
        }

        // Administrator fan zone
        [Route("{userItemId:guid}")]
        [ResponseType(typeof(UserItemDTO))]
        public IHttpActionResult Put(int theaterId, Guid userItemId, UserItem userItem)
        {
            var updateItem = _userItemService.ApproveItem(theaterId, userItemId, userItem);

            if (updateItem == null)
            {
                return BadRequest();
            }

            return Ok(updateItem);
        }

        // Vlasnik itema
        [Route("~/api/UserItems/{userItemId:guid}")]
        public UserItemDTO Put(Guid userItemId, Bid bid)
        {
            var userItem = _userItemService.SellItem(User.Identity.GetUserId(), userItemId, bid);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return userItem;
        }

        // Administrator fan zone
        [Route("{userItemId:guid}")]
        public void Delete(int theatreId, Guid userItemId)
        {
            _userItemService.RemoveItem(theatreId, userItemId);
        }
    }
}
