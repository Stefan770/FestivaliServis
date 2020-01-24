using System.Collections.Generic;
using FestivaliServis.Models;

namespace FestivaliServis.Repository
{
    public interface IMestoRepo
    {
        IEnumerable<Mesto> GetAll();
        Mesto GetById(int id);
        IEnumerable<Mesto> GetByZip(int kod);
    }
}