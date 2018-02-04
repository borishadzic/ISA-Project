using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IPlayService
    {
        IEnumerable<PlayDTO> Get(int theaterId);
        PlayDTO Get(int theaterId, int playId);
        PlayDTO Add(int theaterId, Play play);
        PlayDTO Update(int theaterId, int playId, Play play);
        void Remove(int theaterId, int playId);


    }
}
