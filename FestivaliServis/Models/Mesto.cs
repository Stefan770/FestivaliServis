using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivaliServis.Models
{
    public class Mesto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        [Range(0, 99999)]
        public int Zip { get; set; }
    }
}