using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin
    {
        public int id { get; set; }

        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavnet kan ikke være tom og må være kun bokstaver")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå]{1,30}")]
        public string Brukernavn { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passordet kan ikke være tom")]
        public string Passord { get; set; }
    }
}
