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
        IEnumerable<PlayDTO> GetRepertoire(int theaterId);
        PlayDTO Get(int playId);
        PlayDTO Add(Play play);
        PlayDTO Update(int playId, Play play);
        void Remove(int playId);
    }
}
