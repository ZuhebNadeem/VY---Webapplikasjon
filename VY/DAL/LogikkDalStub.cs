using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class LogikkDalStub : ILogikkDAL
    {
        public bool finnesAdmin(Admin InnAdmin)
        {
            throw new NotImplementedException();
        }

        public bool Registrer(Admin InnAdmin)
        {
            if (InnAdmin.Brukernavn == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RegistrerSuper()
        {
            throw new NotImplementedException();
        }

        public Admin HentAdmin(int id)
        {
            if (id == 0)
            {
                var admin1 = new Admin();
                admin1.id = 0;
                return admin1;
            }

            var admin = new Admin()
            {
                id = 1,
                Brukernavn = "test",
                Passord = "testPass"
            };
            return admin;
        }

        public List<Adminer> ListAdminer()
        {
            var adminListe = new List<Adminer>();

            var admin = new Adminer()
            {
                Id = 2,
                Brukernavn = "test",
                Passord = new byte[] {},
                Salt = "t"
            };
            
            adminListe.Add(admin);
            adminListe.Add(admin);
            adminListe.Add(admin);

            return adminListe;
           
        }

        public bool EndreAdmin(int id, Admin InnAdmin)
        {
            if (id == 0)
            {
                return false;
            }
            
            return true;
            
        }

        public bool SlettAdmin(int id)
        {
            if (id == 0)
            {
                return false;
            }

            return true;
        }

        public List<StasjonAdmin> ListStasjoner()
        {

            var liste = new List<StasjonAdmin>();

            var stasjonsadmin = new StasjonAdmin()
            {
                id = 2,
                Navn = "Oslo S",
                LinjeListe=new List<LinjeBasicAdmin>()
             



            };

            liste.Add(stasjonsadmin);
            liste.Add(stasjonsadmin);
            liste.Add(stasjonsadmin);

            return liste;

        }

        public bool SlettStasjon(int id)
        {
            if (id == 0)
            {
                return false;
            }

            return true;
        }

        public StasjonRegAdmin HentStasjon(int id)
        {
            if (id == 0) 
            {
                var Stasjon1 = new StasjonRegAdmin();

                Stasjon1.id = 0;

                return Stasjon1;
            }

            var Stasjon2 = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "Løren",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()

            };
            return Stasjon2;
        }

        public StasjonRegAdmin HentRegStasjon()
        {



            StasjonRegAdmin nyregAdmin = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "test",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()


            };


        





            return nyregAdmin;




        }


        public bool EndreStasjon(StasjonRegAdmin innStasjon)
        {
            if (innStasjon.id == 0)
            {
                return false;
            }

            return true;

        }


        public bool RegistrerStasjon(StasjonRegAdmin InnStasjon)
        {

            if (InnStasjon.id == 0)
            {
                return false;
            }

            return true;
        }

        public List<LinjeAdmin> ListLinjer()
        {
            var liste = new List<LinjeAdmin>();

            var linjeadmin = new LinjeAdmin()
            {
                id = 2,
                Navn = "L1",
                Pris = 200,
                Stasjoner = new List<string>(),
                Tid=2

            };

            liste.Add(linjeadmin);
            liste.Add(linjeadmin);
            liste.Add(linjeadmin);

            return liste;



        }

        public LinjeAdmin HentLinje(int id)
        {
            if (id == 0)
            {
                var linje1 = new LinjeAdmin() {
                    id = 0,
                    Navn = "test",
                    Stasjoner = new List<string>(),
                    Pris = 200,
                    Tid = 2
                

                    };

                return linje1;

            }


            var linje = new LinjeAdmin()
            {

                id = 1,
                Navn = "L4",
                Stasjoner = new List<string>(),
                Pris = 200,
                Tid = 2

            };

            return linje;





        }


        public LinjeRegAdmin HentRegLinje()
        {


            LinjeRegAdmin nyregAdmin = new LinjeRegAdmin()
            {
                Navn = "test",
                Pris=200, 
                AlleStasjoner=new List<StasjonAdmin>(),
                Tid=2,
                ValgteStasjoner=new List<int>()


            };

            return nyregAdmin;

        }

        public bool EndreLinje(LinjeAdmin innLinje)
        {
            if (innLinje.id == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteLinje(int id)
        {
            if (id == 0)
            {
                return false;
            }

            return true;
        }

        public bool RegistrerLinje(LinjeRegAdmin nyLinje)
        {
            if (nyLinje.Navn=="")
            {
                return false;
            }

            return true;
        }



        //KUNDER
        public List<Kunder> ListKunder()
        {
            var liste= new List<Kunder>();
            var kunde = new Kunder()
            {
                Id = 1,
                BetalingsMetode = "Vipps",
                Epost = "Ole-hansen@hotmail.com",
                Telefon = "47281929"
            };

            liste.Add(kunde);
            liste.Add(kunde);
            liste.Add(kunde);

            return liste;

        }

        public KundeAdmin HentKunde(int id)
        {
            if (id == 0)
            {
                var kunde1 = new KundeAdmin()
                {
                   id=0,
                   Epost="Ole-hansen@hotmail.com",
                   Telefon="12345678"


                };

                return kunde1;

            }


            var kunde = new KundeAdmin()
            {

                id = 1,
                Epost = "Ole-hansen@hotmail.com",
                Telefon = "12345678"

            };

            return kunde;





        }

        public bool EndreKunde(KundeAdmin innKunde)
        {
            if (innKunde.id == 0)
            {
                return false;
            }

            return true;

        }

        public bool DeleteKunde(int id)
        {
            if (id == 0)
            {
                return false;

            }

            return true;

        
        }

        //METODER for hashing og salt

        //Metode for å hashe et passordet
        private static byte[] lagHash(string innVerdi)
        {
            byte[] inndata, utdata;

            var algoritme = SHA256.Create();
            inndata = Encoding.UTF8.GetBytes(innVerdi);
            utdata = algoritme.ComputeHash(inndata);

            return utdata;
        }

        //Metode for å lage salt
        private static string lagSalt()
        {
            byte[] randomArray = new byte[10];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);

            string randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

    }
}
