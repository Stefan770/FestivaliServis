using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivaliServis.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        [Range(1, double.MaxValue)]
        public double Cena { get; set; }
        [Range(1951, 2017)]
        public int Godina { get; set; }

        public int MestoId { get; set; }
        public Mesto Mesto { get; set; }
    }
}