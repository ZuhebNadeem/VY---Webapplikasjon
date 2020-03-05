using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StasjonRegAdmin
    {
        public int id { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Stasjons sitt navn, kan ikke være tom og må være kun bokstaver")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå]{1,30}")]
        public string Navn { get; set; }

        [Display(Name="ValgteLinjer")]
        [Required(ErrorMessage = "Huk av for minst 1 linje")]
        public IList<int> ValgteLinjer { get; set; }
        public IList<LinjeBasicAdmin> AlleLinjer { get; set; }
    }
}
