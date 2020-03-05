namespace VY.Models.Domene
{
    public class Rute
    {
        public int Id { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
        public string FraStasjon { get; set; }
        public int FraIndeks { get; set; }
        public string TilStasjon { get; set; }
        public int TilIndeks { get; set; }
        public string Linje { get; set; }
        public string LinjeRetur { get; set; }

        public string Pris { get; set; }

        public bool TurRetur { get; set; }
        public string ReturDato { get; set; }
        public string ReturTid { get; set; }

    }


}