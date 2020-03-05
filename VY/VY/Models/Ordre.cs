namespace VY.Models
{
    public class Ordre
    {

        public string Dato { get; set; }
        
        public string Tid { get; set; }
        
        public string FraStasjon { get; set; }
        public string TilStasjon { get; set; }
        public int FraIndeks { get; set; }
        public int TilIndeks { get; set; }
        public string Pris { get; set; }
        public string Reisende { get; set; }
        public string Sum { get; set; }
        public bool TurRetur { get; set; }

        public string ReturDato { get; set; }

        public string ReturTid { get; set; }

    }
}