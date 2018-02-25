using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class TheatersController : ApiController
    {
        // TODO: Dodaj ogranicenje da samo admini mogu da dodaju, menjaju i brisu theatre
        private readonly ITheaterService _theaterService;

        public TheatersController(ITheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        // api/theaters/?type=cinemas
        public IHttpActionResult Get(string type = "all")
        {
            if (type == "all")
            {
                return Ok(_theaterService.GetAll());
            }
            else if (type == "cinemas") 
            {
                return Ok(_theaterService.GetAllCinemas());
            }
            else if (type == "plays")
            {
                return Ok(_theaterService.GetAllPlayTheaters());
            } 
            else
            {
                return BadRequest();
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

        public TheaterDTO Post(Theater theater)
        {
            return _theaterService.Add(theater);
        }

        public IHttpActionResult Put(int id, Theater theater)
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

        public void Delete(int id)
        {
            _theaterService.Remove(id);
        }
    }
}
