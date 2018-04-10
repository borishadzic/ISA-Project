using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Filters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Theaters/{theaterId}/Items")]
    public class ItemsController : ApiController
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Route("")]
        public IEnumerable<ItemDTO> Get(int theaterId)
        {
            return _itemService.GetItemsForTheater(theaterId);
        }

        [Route("{itemId}")]
        public ItemDTO Get(int theaterId, Guid itemId)
        {
            return _itemService.GetItem(theaterId, itemId);
        }

        [Route("")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public IEnumerable<ItemDTO> GetSold(int theaterId, [FromUri] bool sold)
        {
            return _itemService.GetBoughtItemsForTheater(theaterId);
        }

        [Route("")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public  IHttpActionResult Post(int theaterId, Item item)
        {
            var newItem = _itemService.AddItem(theaterId, item);

            if (newItem == null)
            {
                return BadRequest();
            }

            return Ok(newItem);
        }

        [Route("{itemId}")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public async Task<ItemDTO> PostImageAsync(int theaterId, Guid itemId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var item = await _itemService.SetImageAsync(theaterId, itemId, HttpContext.Current.Request.Files["image"]);

            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return item;
        }

        [Route("{itemId}")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public ItemDTO Put(int theaterId, Guid itemId, Item item)
        {
            return _itemService.UpdateItem(theaterId, itemId, item);
        }

        [Route("{itemId}")]
        [ISofAAuthorization(Role = ISofAUserRole.FanZoneAdmin)]
        public void Delete(int theaterId, Guid itemId)
        {
            _itemService.RemoveItem(theaterId, itemId);
        }

        [HttpPost]
        [Route("~/api/Items")]
        public IHttpActionResult BuyItems(IEnumerable<Item> items)
        {
            var success = _itemService.BuyItems(items, User.Identity.GetUserId());

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
