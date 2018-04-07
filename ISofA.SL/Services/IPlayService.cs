using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IPlayService
    {
        IEnumerable<PlayDTO> GetAll(int theaterId);
        PlayDTO Get(int playId);
        PlayDTO Add(int theaterId, Play play);
        PlayDTO Update(int playId, Play play);
        void Remove(int playId);
    }
}
