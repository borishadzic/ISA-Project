﻿using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class TheaterPantry : ITheaterPantry
    {
        public IEnumerable<Theater> GetAll()
        {
            using(var context = new ISofADbContext())
            {
                return context.Theaters.ToList();
            }
        }

        public Theater Get(int theaterId)
        {
            using (var context = new ISofADbContext())
            {
                Theater t = context.Theaters.AsNoTracking().Where(x => x.TheaterId == theaterId).FirstOrDefault();
                if (t != null)
                    return new Theater() { TheaterId = t.TheaterId, Name = t.Name };
                return null;
            }
        }

        public Theater Add(Theater theater)
        {
            using (var context = new ISofADbContext())
            {
                var addedTheater = context.Theaters.Add(theater);
                context.SaveChanges();
                return addedTheater;
            }
        }
    }
}
