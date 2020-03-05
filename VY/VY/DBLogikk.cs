using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DAL;
using Model;
using VY.Models;
using VY.Models.Domene;

namespace VY
{
    public class DBLogikk
    {
        public List<Stasjoner> alleStasjoner()
        {
            List<Stasjoner> stasjonerList;
            using (var db = new DB())
            {
                stasjonerList = db.Stasjoner.ToList();
                
            }
            return stasjonerList;
        }

        public List<Stasjoner> alleStasjoner(Stasjoner stasjon)
        {
            List<Stasjoner> alleStasjoner;
            List<Stasjoner> alleUtenomValgtStasjoner = new List<Stasjoner>();

            using (var db = new DB())
            {
                alleStasjoner = db.Stasjoner.ToList();
                foreach (var s in alleStasjoner)
                {
                    if (!s.Equals(stasjon))
                    {
                        alleUtenomValgtStasjoner.Add(s);
                    }
                }
            }

            return alleUtenomValgtStasjoner;
        }


        public string jsonAlleStasjoner()
        {
            List<Stasjoner> stasjonerList = this.alleStasjoner();

            var alleStasjoner = new List<Stasjon>();

            foreach (var s in stasjonerList)
            {
                var stasjon = new Stasjon();
                stasjon.id = s.Id;
                stasjon.Navn = s.Navn;
                alleStasjoner.Add(stasjon);
            }


            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);


            return json;
        }



        public Ordre HentOrdre(string Dato, string Tid, string FraStasjon, string TilStasjon, string AntVoksne,
            string AntBarn, string TurRetur, string ReturDato, string ReturTid)
        {
            using (var db = new DB())
            {
                var fra = db.Stasjoner.Find(int.Parse(FraStasjon));
                var til = db.Stasjoner.Find(int.Parse(TilStasjon));

                DateTime tilDateTime = new DateTime();

                DateTime fraDateTime = DateTime.ParseExact(Dato + " " + Tid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                if (bool.Parse(TurRetur))
                {
                    tilDateTime = DateTime.ParseExact(ReturDato + " " + ReturTid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }

                var unikeLinjerListe = unikeLinjer(FraStasjon, TilStasjon);
                var pris = 0;
                foreach (var l in unikeLinjerListe)
                {
                    pris += l.Pris;
                }

                var prisRute = pris * int.Parse(AntVoksne) + (pris / 2) * int.Parse(AntBarn) + " Kr";
                var ReisendeTekst = AntVoksne + " Voksne og " + AntBarn + " Barn";

                var sumOrdre = pris * int.Parse(AntVoksne) + (pris / 2) * int.Parse(AntBarn);
                if (bool.Parse(TurRetur))
                {
                    sumOrdre += sumOrdre;
                }
                
                var utOrdre = new Ordre()
                {

                    Dato = Dato,
                    Tid = fraDateTime.ToString("t"),
                    FraStasjon = fra.Navn,
                    FraIndeks = int.Parse(FraStasjon),
                    TilStasjon = til.Navn,
                    TilIndeks = int.Parse(TilStasjon),
                    Pris = prisRute,
                    Reisende = ReisendeTekst,
                    Sum = sumOrdre + " Kr",
                    TurRetur = bool.Parse(TurRetur),
                    ReturDato = ReturDato,
                    ReturTid = tilDateTime.ToString("t")
                };

                return utOrdre;
            }
        }

        public List<Rute> hentRute(string Dato, string Tid, string FraStasjon, string TilStasjon, string Linje, string AntVoksen, string AntBarn, string TurRetur, string ReturDato, string ReturTid)
        {
            using (var db = new DB())
            {

                var fra = db.Stasjoner.Find(int.Parse(FraStasjon));
                var til = db.Stasjoner.Find(int.Parse(TilStasjon));

                var unikeLinjerListe = unikeLinjer(FraStasjon, TilStasjon);
                var unikeReturLinjerListe = unikeLinjer(TilStasjon, FraStasjon);

                var pris = 0;
                foreach (var l in unikeLinjerListe)
                {
                    pris += l.Pris;
                }

                var totPris = pris*int.Parse(AntVoksen) + (pris/2)*int.Parse(AntBarn);

                var linjeTekst = unikeLinjerTekst(FraStasjon, TilStasjon, unikeLinjerListe);
                var linjeReturTekst = unikeLinjerTekst(FraStasjon, TilStasjon, unikeReturLinjerListe);

                List<Rute> avgangerListe = new List<Rute>();

                DateTime tilDateTime = new DateTime();

                DateTime fraDateTime = DateTime.ParseExact(Dato + " " + Tid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                if (bool.Parse(TurRetur))
                {
                    tilDateTime = DateTime.ParseExact(ReturDato + " " + ReturTid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

                }
                var fraIntervall = 1;
                var tilIntervall = 1;
                if (!(unikeLinjerListe.Count == 0 || unikeReturLinjerListe.Count == 0))
                {
                    fraIntervall = unikeLinjerListe.First().Tid;
                    tilIntervall = unikeReturLinjerListe.First().Tid;
                }

                for (int i = 0; i < 5; i++)
                {
                    fraDateTime = fraDateTime.AddMinutes(fraIntervall);
                    if (bool.Parse(TurRetur))
                    {
                        tilDateTime = tilDateTime.AddMinutes(tilIntervall);
                    }
                    var utRute = new Rute()
                    {

                        Id = i,
                        Dato = Dato,
                        Tid = fraDateTime.ToString("t"),
                        FraStasjon = fra.Navn,
                        FraIndeks = int.Parse(FraStasjon),
                        TilStasjon = til.Navn,
                        TilIndeks = int.Parse(TilStasjon),
                        Linje = linjeTekst,
                        LinjeRetur = linjeReturTekst,
                        Pris = totPris + " Kr",
                        TurRetur = bool.Parse(TurRetur),
                        ReturDato = ReturDato,
                        ReturTid = tilDateTime.ToString("t")

                    };
                    avgangerListe.Add(utRute);
                }

                return avgangerListe;
            }
        }

        public List<Linjer> unikeLinjer(string FraStasjon, string TilStasjon)
        {
            

            using (var db = new DB())
            {
                List<Linjer> linjeListe = new List<Linjer>();

                var fra = db.Stasjoner.Find(int.Parse(FraStasjon));
                var til = db.Stasjoner.Find(int.Parse(TilStasjon));

                if (fra.Linjer.Count == 2)
                {
                    foreach (var l in fra.Linjer)
                    {
                        if (til.Linjer.Contains(l))
                        {
                            linjeListe.Add(l);
                        }
                    }
                }
                else if (til.Linjer.Count == 2)
                {
                    foreach (var l in til.Linjer)
                    {
                        if (fra.Linjer.Contains(l))
                        {
                            linjeListe.Add(l);
                        }
                    }
                }
                else
                {
                    foreach (var l in fra.Linjer)
                    {
                        if (!linjeListe.Contains(l))
                        {
                            linjeListe.Add(l);
                        }
                        
                    }
                    foreach (var l in til.Linjer)
                    {
                        if (!linjeListe.Contains(l))
                        {
                            linjeListe.Add(l);
                        }
                    }
                }

                return linjeListe;
            }
            
        }


        public string unikeLinjerTekst(string FraStasjon, string TilStasjon, List<Linjer> linjeList)
        {
            string ut = "";
            List<Linjer> linjeListe = linjeList;

            if (linjeListe.Count == 1)
            {
                ut += linjeListe[0].Navn;
            }
            else
            {
                ut += (linjeListe.Count-1) + " bytte: ";

                for (int i = 0; i < linjeListe.Count; i++)
                {
                    if (i == (linjeListe.Count - 1))
                    {
                        ut += linjeListe[i].Navn;
                    }
                    else
                    {
                        ut += linjeListe[i].Navn + " -> ";
                    }

                }
            }



            return ut;
        }



        public bool lagreOrdre(Bestilling Bestilling)
        {
            using (var db = new DB())
            {

                DateTime utreiseDateTime = DateTime.ParseExact(Bestilling.Dato + " " + Bestilling.Tid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                Stasjoner fraStasjon = db.Stasjoner.Find(int.Parse(Bestilling.FraIndeks));
                Stasjoner tilStasjon = db.Stasjoner.Find(int.Parse(Bestilling.TilIndeks));

                DateTime returDateTime = new DateTime();

                if (Bestilling.TurRetur)
                {
                    returDateTime = DateTime.ParseExact(Bestilling.ReturDato + " " + Bestilling.ReturTid, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }

                var fraRute = new Ruter()
                {
                    Dato = utreiseDateTime,
                    FraStasjon = fraStasjon.Navn,
                    TilStasjon = tilStasjon.Navn
                };

                var tilRute = new Ruter()
                {
                    Dato = returDateTime,
                    FraStasjon = tilStasjon.Navn,
                    TilStasjon = fraStasjon.Navn
                };

                var unikeLinjerListe = unikeLinjer(Bestilling.FraIndeks, Bestilling.TilIndeks);
                var pris = 0;
                foreach (var l in unikeLinjerListe)
                {
                    pris += l.Pris;
                }

                var sum = (pris * int.Parse(Bestilling.AntVoksne) + (pris / 2) * int.Parse(Bestilling.AntBarn));

                var ordreListe = new List<Ruter>();
                ordreListe.Add(fraRute);
                if (Bestilling.TurRetur)
                {
                    ordreListe.Add(tilRute);
                    sum += sum;
                }
                
                

                var nyOrdre = new Ordrer()
                {
                    Ruter = ordreListe,
                    Sum = sum
                };

                var kundeOrdreListe = new List<Ordrer>();
                kundeOrdreListe.Add(nyOrdre);

                var kunde = new Kunder()
                {
                    BetalingsMetode = Bestilling.BetalingsMetode,
                    Epost = Bestilling.Epost,
                    Telefon = Bestilling.Telefon
                    //Ordrer = kundeOrdreListe
                };

                
                try
                {

                    // lagre Stasjoner
                    db.Ordrer.Add(nyOrdre);
                    db.Kunder.Add(kunde);
                    db.SaveChanges();
                }
                catch (Exception feil)
                {
                    return false;
                }
                return true;
            }

        }



    }
}