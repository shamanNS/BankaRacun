using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predrag_Djokic.Models
{
    public class Uplatnica
    {
        public int Id { get; set; }
        public Racun Racun { get; set; }
        public double Iznos { get; set; }
        public DateTime DatumPrometa { get; set; }

        public string SvrhaUplate { get; set; }
        public string Uplatilac { get; set; }
        public bool Hitno { get; set; }

        public Uplatnica()
        {

        }
    }
}