using FestivaliServis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace FestivaliServis.Repository
{
    public class FestivalRepo : IDisposable, IFestivalRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Festival> GetAll()
        {
            return db.Festivali.Include(x => x.Mesto).OrderByDescending(x => x.Cena);
        }

        public IEnumerable<Festival> GetByYear(FestivalFilter filter)
        {
            return db.Festivali.Include(x => x.Mesto)
                .Where(x => x.Godina >= (filter.Start ?? 0) && x.Godina <= (filter.Kraj ?? 2017))
                .OrderBy(x => x.Godina);
        }

        public Festival GetById(int id)
        {
            return db.Festivali.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Festival nekretnina)
        {
            db.Festivali.Add(nekretnina);
            db.SaveChanges();
        }

        public void Update(Festival nekretnina)
        {
            db.Entry(nekretnina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Festival nekretnina)
        {
            db.Festivali.Remove(nekretnina);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}