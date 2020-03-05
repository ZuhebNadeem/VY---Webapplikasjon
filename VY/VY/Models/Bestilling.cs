namespace VY.Models
{
    public class Bestilling
    {
        public string Dato { get; set; }
        public string Tid { get; set; }
        public string FraIndeks { get; set; }
        public string TilIndeks { get; set; }
        public string AntVoksne { get; set; }
        public string AntBarn { get; set; }
        public bool TurRetur { get; set; }
        public string ReturDato { get; set; }
        public string ReturTid { get; set; }
        public string Epost { get; set; }
        public string Telefon { get; set; }
        public string BetalingsMetode { get; set; }



    }
}