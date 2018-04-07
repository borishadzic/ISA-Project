using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.Claims;
using ISofA.SL.DTO;
using ISofA.SL.Exceptions;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class PlayService : IdentityService, IPlayService
    {

        public PlayService(IUnitOfWork unitOfWork, IIdentity identity) : base(unitOfWork, identity)
        {
        }

        public PlayDTO Add(Play play)
        {
            // todo exists theaterid? authorize
            if (!_identity.IsAuthorizedTheaterAdmin(play.TheaterId))
                throw new UnauthorizedRequestException();
            play = UnitOfWork.Plays.Add(play);
            UnitOfWork.SaveChanges();

            return new PlayDTO(play);
        }

        public IEnumerable<PlayDTO> GetRepertoire(int theaterId)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);
            if (theater == null)
                throw new TheaterNotFoundException(theaterId);

            return UnitOfWork.Plays.Find(x => x.TheaterId == theaterId)
                .Select(x => new PlayDTO(x));
        }

        public PlayDTO Get(int playId)
        {
            Play play = UnitOfWork.Plays.Get(playId);

            if (play == null)
                throw new PlayNotFoundException(playId);

            return new PlayDTO(play);
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
            return new PlayDTO(modified);
        }
    }
}