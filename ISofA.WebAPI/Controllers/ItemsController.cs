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
        [Route("api/Theaters/{theaterId}/Items/Bought")]
        public IEnumerable<ItemDTO> GetBought(int theaterId)
        {
            return _itemService.GetBoughtItemsForTheater(theaterId);
        }

        [Route("api/Theaters/{theaterId}/Items")]
        public async Task<IHttpActionResult> PostAsync(int theaterId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new CustomMultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath("~/Images"));
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var formDataKeys = result.FormData.AllKeys;

            if (!formDataKeys.Contains("Name") ||
                !formDataKeys.Contains("Description") ||
                !formDataKeys.Contains("Price"))
            {
                return BadRequest("Name, description and price are required values!");
            }

            var name = result.FormData.Get("Name");
            var description = result.FormData.Get("Description");
            var success = float.TryParse(result.FormData.Get("Price"), out float price);

            if (!success || name.Length < 3 || description.Length < 3)
            {
                return BadRequest("Invalid form data.");
            }

            var item = new Item() { Name = name, Description = description, Price = price };

            if (result.FileData.Count > 0)
            {
                var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
                item.ImageUrl = GetImageUrl(uploadedFileInfo.Name);
            }
            else
            {
                item.ImageUrl = GetImageUrl();
            }

            return Ok(_itemService.AddItem(theaterId, item));
        }

        private string GetImageUrl(String fileName = null)
        {
            //string rootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{ Url.Content("~/") }Images/{ fileName ?? "default.png" }";
        }

        [Route("api/Items/{itemId}")]
        public ItemDTO Get(Guid itemId)
        {
            return _itemService.GetItem(itemId);
        }

        [HttpPost]
        [Authorize]
        [Route("api/Items/{itemId}/Buy")]
        public IHttpActionResult BuyItem(Guid itemId)
        {
            var success = _itemService.BuyItem(itemId, User.Identity.GetUserId());

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
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string file = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            return (Guid.NewGuid().ToString() + Path.GetExtension(file)).Replace("-", "_");
        }
    }
}
