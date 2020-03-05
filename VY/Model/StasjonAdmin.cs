using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Model
{
   public class StasjonAdmin
    {
        public int id { get; set; }
        public string Navn { get; set; }
        public List<LinjeBasicAdmin> LinjeListe { get; set; }
    }
}
