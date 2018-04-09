using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ISofA.SL.Implementations
{
    public class TheaterService : Service, ITheaterService
    {
        public TheaterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TheaterListDTO Add(Theater theater)
        {
            UnitOfWork.Theaters.Add(theater);
            UnitOfWork.SaveChanges();
            return new TheaterListDTO(theater);
        }

        public TheaterDetailDTO Get(int theaterId)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
            {
                // TODO: Izbaci exception ako ne pronadje theater
                return null;
            }

            return theater;
        }

        public IEnumerable<TheaterListDTO> GetAll()
        {
            return UnitOfWork.Theaters.GetAll().Select(t => new TheaterListDTO(t));
        }

        public IEnumerable<TheaterListDTO> GetAllCinemas()
        {
            return UnitOfWork.Theaters.Find(t => t.Type == TheaterType.Cinema).Select(t => new TheaterListDTO(t));
        }

        public IEnumerable<TheaterListDTO> GetAllPlayTheaters()
        {
            return UnitOfWork.Theaters.Find(t => t.Type == TheaterType.Play).Select(t => new TheaterListDTO(t));
        }

        public void Remove(int theaterId)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater != null)
            {
                UnitOfWork.Theaters.Remove(theater);
                UnitOfWork.SaveChanges();
            }
        }

        public TheaterListDTO Update(int theaterId, Theater update)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
            {
                // TODO: Izbaci exception ako ne pronadje theater
                return null;
            }

            theater.Name = update.Name;
            theater.Latitude = update.Latitude;
            theater.Longitude = update.Longitude;
            theater.Type = update.Type;
            UnitOfWork.SaveChanges();
            return new TheaterListDTO(theater);
        }
    }
}
