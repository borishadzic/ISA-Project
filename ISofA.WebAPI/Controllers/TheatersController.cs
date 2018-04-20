using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Filters;
using ISofA.WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class TheatersController : ApiController
    {
        private readonly ITheaterService _theaterService;

        public TheatersController(ITheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        // api/theaters?type=cinemas
        public IEnumerable<TheaterListDTO> Get(string type = "")
        {
            if (string.IsNullOrEmpty(type))
            {
                return _theaterService.GetAll();
            }
            else if (type == "cinemas") 
            {
                return _theaterService.GetAllCinemas();
            }
            else if (type == "theaters")
            {
                return _theaterService.GetAllPlayTheaters();
            } 
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }   
        }

        public IHttpActionResult Get(int id)
        {
            var theater = _theaterService.Get(id);

            if (theater != null)
            {
                return Ok(theater);
            }
            else
            {
                return NotFound();
            }
        }

        [ISofAAuthorization(Role = ISofAUserRole.SysAdmin)]
        public TheaterListDTO Post(Theater theater)
        {
            return _theaterService.Add(theater);
        }

        [ISofAAuthorization(Role = ISofAUserRole.SysAdmin)]
        public IHttpActionResult Put(int id, TheaterBindingModel theater)
        {
            var updatedTheater = _theaterService.Update(id, theater);

            if (updatedTheater == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedTheater);
            }
        }

        [ISofAAuthorization(Role = ISofAUserRole.SysAdmin)]
        public void Delete(int id)
        {
            _theaterService.Remove(id);
        }
    }
}
