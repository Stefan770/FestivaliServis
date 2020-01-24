using System.Collections.Generic;
using FestivaliServis.Models;

namespace FestivaliServis.Repository
{
    public interface IFestivalRepo
    {
        void Add(Festival nekretnina);
        void Delete(Festival nekretnina);
        IEnumerable<Festival> GetAll();
        Festival GetById(int id);
        IEnumerable<Festival> GetByYear(FestivalFilter filter);
        void Update(Festival nekretnina);
    }
}