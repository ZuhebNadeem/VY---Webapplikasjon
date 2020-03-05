using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class LinjeRegAdmin
    {
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Linje sitt navn må inneholde minst to bokstaver eller to tall, eller en blandning")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå 0-9]{2,30}")]
        public string Navn { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Pris må være større enn null")]
        [RegularExpression(@"[0-9]{1,5}")]
        public int Pris { get; set; }

        [Display(Name = "Tid")]
        [Required(ErrorMessage = "Tid må være større enn null")]
        [RegularExpression(@"[0-9]{1,5}")]
        public int Tid { get; set; }


        [Display(Name = "ValgteStasjoner")]
        public IList<int> ValgteStasjoner { get; set; }
        public IList<StasjonAdmin> AlleStasjoner { get; set; }
    }
}
