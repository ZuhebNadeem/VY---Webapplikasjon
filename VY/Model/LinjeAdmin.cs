using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LinjeAdmin
    {
        public int id { get; set; }


        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Linje sitt navn må inneholde minst to bokstaver eller to tall, eller en blandning")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå 0-9]{2,30}")]
        public string Navn { get; set; }

        [Display(Name = "Pris ")]
        [Required(ErrorMessage = "Pris må være et tall")]
        [RegularExpression(@"[0-9]{1,5}")]
        public int Pris { get; set; }

        [Display(Name = "Tid ")]
        [Required(ErrorMessage = "Tid må være")]
        [RegularExpression(@"[0-9]{1,5}")]
        public int Tid { get; set; }
        public List<String> Stasjoner { get; set; }


    }
}
