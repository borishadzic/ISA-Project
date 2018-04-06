using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        // TODO: Dodaj ogranicenje za odredjene korisnike. FanZoneAdmin i ulogovani korisnik...
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Route("api/Theaters/{theaterId}/Items")]
        public IEnumerable<ItemDTO> Get(int theaterId)
        {
            return _itemService.GetItemsForTheater(theaterId);
        }

        [HttpGet]
        [Route("api/Theaters/{theaterId}/Items")]
        public IEnumerable<ItemDTO> GetBought(int theaterId, [FromUri] bool sold)
        {
            return _itemService.GetBoughtItemsForTheater(theaterId);
        }

        [Route("api/Theaters/{theaterId}/Items")]
        public  IHttpActionResult Post(int theaterId, Item item)
        {
            var newItem = _itemService.AddItem(theaterId, item);

            if (newItem == null)
            {
                return BadRequest();
            }

            return Ok(newItem);
        }

        [HttpPost]
        [Route("~/api/Items/{itemId}")]
        public async Task<ItemDTO> UploadImageAsync(Guid itemId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var item = await _itemService.SetImageAsync(itemId, HttpContext.Current.Request.Files["image"]);

            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return item;
        }

        //private string GetImageUrl(String fileName = null)
        //{
        //    //string rootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        //    return $"{ Url.Content("~/") }Images/{ fileName ?? "default.png" }";
        //}

        [Route("api/Items/{itemId}")]
        public ItemDTO Get(Guid itemId)
        {
            return _itemService.GetItem(itemId);
        }

        [HttpPost]
        [Authorize]
        [Route("api/Items/Buy")]
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

        [Route("api/Items/{itemId}")]
        public ItemDTO Put(Guid itemId, Item item)
        {
            return _itemService.UpdateItem(itemId, item);
        }

        [Route("api/Items/{itemId}")]
        public void Delete(Guid itemId)
        {
            _itemService.RemoveItem(itemId);
        }
    }

    // TODO: Privremeno sam stavio klasu ovde. Ne znam gde drugde da je stavim. Premesti je ako zelis
    //public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    //{
    //    public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

    //    public override string GetLocalFileName(HttpContentHeaders headers)
    //    {
    //        string file = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
    //        return (Guid.NewGuid().ToString() + Path.GetExtension(file)).Replace("-", "_");
    //    }
    //}
}
