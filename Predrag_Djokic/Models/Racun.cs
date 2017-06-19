using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Predrag_Djokic.Models
{
    public class Racun
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nosilac racuna")]
        public string NosilacRacuna { get; set; }

        [Required]
        [Display(Name = "Broj racuna")]
        public string BrojRacuna { get; set; }

        [Display(Name="Aktivan")]
        public bool Aktivan { get; set; }

        [Display(Name = "Online")]
        public bool OnlineBanking { get; set; }
        public List<Uplatnica> Uplatnice { get; set; }

        public Racun()
        {
            this.Uplatnice = new List<Uplatnica>();
        }
    }
}