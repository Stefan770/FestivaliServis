using FestivaliServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivaliServis.Repository
{
    public class MestoRepo : IDisposable, IMestoRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Mesto> GetAll()
        {
            return db.Mesta;
        }

        public Mesto GetById(int id)
        {
            return db.Mesta.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Mesto> GetByZip(int kod)
        {
            return db.Mesta.Where(x => x.Zip < kod).OrderBy(x => x.Zip);
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