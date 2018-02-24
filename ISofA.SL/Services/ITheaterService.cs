using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface ITheaterService
    {
        IEnumerable<TheaterDTO> GetAll(); 
        IEnumerable<TheaterDTO> GetAllCinemas();
        IEnumerable<TheaterDTO> GetAllPlayTheaters();
        TheaterDTO Get(int theaterId);
        TheaterDTO Add(Theater theater);
        TheaterDTO Update(int theaterId, Theater theater);
        void Remove(int theaterId);
    }
}
