using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class PlayService : Service, IPlayService
    {
        public PlayService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PlayDTO Add(int theaterId, Play play)
        {
            play.TheaterId = theaterId;
            play = UnitOfWork.Pantry<Play>().Add(play);
            UnitOfWork.SaveChanges();

            return new PlayDTO(play);
        }

        IEnumerable<PlayDTO> IPlayService.Get(int theaterId)
        {
            return UnitOfWork.Pantry<Play>().Find(x => x.TheaterId == theaterId)
                .Select(x => new PlayDTO(x));
        }

        public PlayDTO Get(int theaterId, int playId)
        {
            Play play = UnitOfWork.Pantry<Play>().Get(theaterId, playId);
            return new PlayDTO(play);
        }

        public void Remove(int theaterId, int playId)
        {
            IPlayPantry pantry = (IPlayPantry)UnitOfWork.Pantry<Play>();
            pantry.Remove(pantry.Get(theaterId, playId));
            UnitOfWork.SaveChanges();
        }

        public PlayDTO Update(int theaterId, int playId, Play play)
        {
            Play modified = UnitOfWork.Pantry<Play>().Get(theaterId, playId);
            modified.Name = play.Name;
            modified.Actors = play.Actors;
            modified.Genre = play.Genre;
            modified.Director = play.Director;
            modified.DurationMins = play.DurationMins;
            modified.PosterUrl = play.PosterUrl;
            modified.TrailerUrl = play.TrailerUrl;
            modified.Description = play.Description;
            UnitOfWork.Modified(modified);
            UnitOfWork.SaveChanges();
            return new PlayDTO(modified);
        }        
    }
}
