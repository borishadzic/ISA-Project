using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface ITheaterService
    {
        IEnumerable<TheaterListDTO> GetAll(); 
        IEnumerable<TheaterListDTO> GetAllCinemas();
        IEnumerable<TheaterListDTO> GetAllPlayTheaters();
        TheaterDetailDTO Get(int theaterId);
        TheaterListDTO Add(Theater theater);
        TheaterListDTO Update(int theaterId, Theater theater);
        void Remove(int theaterId);
    }
}
