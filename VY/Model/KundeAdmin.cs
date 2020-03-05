using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model
{
    public class KundeAdmin
    {
        public int id { get; set; }


        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon må være 8 siffer")]
        [RegularExpression(@"[0-9]{8}")]
        public string Telefon { get; set; }

        [Display(Name = "Epost")]
        [Required(ErrorMessage = "Epost er ikke riktig")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        public string Epost { get; set; }
    }
}
