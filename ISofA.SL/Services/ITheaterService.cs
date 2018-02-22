using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    interface ITheaterService
    {
        IEnumerable<TheaterDTO> GetAllCinemas();
        IEnumerable<TheaterDTO> GetAllPlayTheaters();
    }
}
