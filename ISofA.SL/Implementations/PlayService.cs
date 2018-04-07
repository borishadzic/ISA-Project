using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Exceptions;
using ISofA.SL.Services;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.SL.Implementations
{
    public class PlayService : Service, IPlayService
    {

        public PlayService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PlayDTO Add(int theaterId, Play play)
        {
            // todo exists theaterid?
            play = UnitOfWork.Plays.Add(play);
            UnitOfWork.SaveChanges();

            return play;
        }

        public IEnumerable<PlayDTO> GetAll(int theaterId)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);
            if (theater == null)
                throw new TheaterNotFoundException(theaterId);

            return UnitOfWork.Plays.Find(x => x.TheaterId == theaterId).Select<Play, PlayDTO>(x => x);
        }

        public PlayDTO Get(int playId)
        {
            Play play = UnitOfWork.Plays.Get(playId);

            if (play == null)
                throw new PlayNotFoundException(playId);

            return play;
        }

        public void Remove(int playId)
        {
            IPlayPantry pantry = (IPlayPantry)UnitOfWork.Plays;
            pantry.Remove(pantry.Get(playId));
            UnitOfWork.SaveChanges();
        }

        public PlayDTO Update(int playId, Play play)
        {
            // todo authorized theaterId

            Play modified = UnitOfWork.Plays.Get(playId);
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
            return modified;
        }
    }
}