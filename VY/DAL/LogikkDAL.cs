using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class LogikkDAL : ILogikkDAL
    {

        Logg logg = new Logg();


  

        //ADMIN-METODER
        public bool finnesAdmin(Admin InnAdmin)
        {

            using (var db = new DB())
            {
                Adminer funnetBruker = db.Adminer.FirstOrDefault(A => A.Brukernavn == InnAdmin.Brukernavn);

             
                if (funnetBruker != null)
                {

                    try
                    {
                        //Sjekker om passordet stemmer med hash+salt i tillegg
                        byte[] passord = lagHash(InnAdmin.Passord + funnetBruker.Salt);
                        bool riktigBruker = funnetBruker.Passord.SequenceEqual(passord);
                        return riktigBruker;
                    }
                    catch (Exception e)
                    {
                        logg.logg_til_exception( e.ToString());
                    
                    }

                }

                return false;

            }

        }

        public bool Registrer(Admin InnAdmin)
        {

            using (var db = new DB())
            {
                
                //Passer på at samme bruker ikke registreres igjen
                Adminer funnetBruker = db.Adminer.FirstOrDefault(A => A.Brukernavn == InnAdmin.Brukernavn); 

                if (funnetBruker == null && !(InnAdmin.Passord == null))
                {
                    var nyAdminDB = new Adminer();

                

                    try
                    {
                        string salt = lagSalt();
                        var passordSalt = InnAdmin.Passord + salt;
                        byte[] passordDB = lagHash(passordSalt);

                        nyAdminDB.Brukernavn = InnAdmin.Brukernavn;
                        nyAdminDB.Passord = passordDB;
                        nyAdminDB.Salt = salt;
                        db.Adminer.Add(nyAdminDB);
                        db.SaveChanges();

                       
                        logg.logg_til_change("En ny bruker med følgende brukernavn ble lagt til " + InnAdmin.Brukernavn);

                        return true;
                    }
                    catch(Exception e)
                    {
                        
                        logg.logg_til_exception(e.ToString());

                    }
                }
                return false;
            }

        }

        public void RegistrerSuper()
        {
            using (var db = new DB())
            {
             

                try
                {
                    var nyAdminDB = new Adminer();

                    string salt = lagSalt();
                    var passordSalt = "admin" + salt;
                    byte[] passordDB = lagHash(passordSalt);

                    nyAdminDB.Brukernavn = "admin";
                    nyAdminDB.Passord = passordDB;
                    nyAdminDB.Salt = salt;

                    var FurkanAdminDB = new Adminer();

                    string Fsalt = lagSalt();
                    var FpassordSalt = "passord" + Fsalt;
                    byte[] FpassordDB = lagHash(FpassordSalt);

                    FurkanAdminDB.Brukernavn = "Furkan";
                    FurkanAdminDB.Passord = FpassordDB;
                    FurkanAdminDB.Salt = Fsalt;


                    db.Adminer.Add(nyAdminDB); //SUPERADMIN
                    db.Adminer.Add(FurkanAdminDB);
                    db.SaveChanges();



                }
                catch(Exception e)
                {
                    logg.logg_til_exception(e.ToString());

                }
            }

        }

        public Admin HentAdmin(int id)
        {
            using (var db = new DB())
            {
                try
                {
                    Adminer dbAdmin = db.Adminer.Find(id);
                    Admin utAdmin = new Admin()
                    {
                        id = dbAdmin.Id,
                        Brukernavn = dbAdmin.Brukernavn,
                        Passord = ""
                    };
                    return utAdmin;
                }
                catch(Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                   return null;
                }
            }
        }

        public List<Adminer> ListAdminer()
        {
            using (var db = new DB())
            {
                try
                {
                    List<Adminer> alleAdminer = db.Adminer.ToList();
                    return alleAdminer;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                  return null;
                }

            }
        }



        //Vi endrer en admin, ved å velge id
        public bool EndreAdmin(int id, Admin InnAdmin)
        {
            using (var db = new DB())
            {
                Adminer funnetBruker = db.Adminer.Find(id);

                Adminer finnesbruker = db.Adminer.FirstOrDefault(A => A.Brukernavn == InnAdmin.Brukernavn);

                string gammeltNavn;
                byte [] gammeltPassord;


                try
                {
                    if (finnesbruker == null || !string.IsNullOrEmpty(finnesbruker.Brukernavn = InnAdmin.Brukernavn))
                    {
                        gammeltNavn = funnetBruker.Brukernavn;
                      

                        funnetBruker.Brukernavn = InnAdmin.Brukernavn;

                    }
                    else
                    {
                        return false;
                    }


                    if (InnAdmin.Passord != null)
                    {
                        string salt = lagSalt();
                        var passordSalt = InnAdmin.Passord + salt;
                        byte[] passordDB = lagHash(passordSalt);

                        funnetBruker.Passord = passordDB;
                        funnetBruker.Salt = salt;
                    }

                    db.SaveChanges();



                    logg.logg_til_change("Admin sitt navn :" + gammeltNavn + " ble endret til nytt brukernavn " + InnAdmin.Brukernavn);

                    

       
                    return true;

                }
                catch(Exception e)
                {
                    logg.logg_til_exception(e.ToString());

                }
            }

            return true;
        }


        //Vi sletter en admin, ved å velge id
        public bool SlettAdmin(int id)
        {
            using (var db = new DB())
            {
                if (id == 1)
                {
                    return false;
                }

                Adminer funnetBruker = db.Adminer.Find(id);

                try
                {
                    db.Adminer.Remove(funnetBruker);
                    db.SaveChanges();

                    logg.logg_til_change("Admin med navn " + funnetBruker.Brukernavn + " ble slettet");
                    return true;
                }
                catch(Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }

        }


        //SLUTT PÅ ADMIN-METODER



        //STASJONER - METODER

        //Vi lister ut alle stasjoner

        public List<StasjonAdmin> ListStasjoner()
        {
            using (var db = new DB())
            {
                List<Stasjoner> alleStasjoner = db.Stasjoner.ToList();
                List<StasjonAdmin> stasjoners = new List<StasjonAdmin>();

                try
                {
                    foreach (var s in alleStasjoner)
                    {
                        List<LinjeBasicAdmin> linjeListe = new List<LinjeBasicAdmin>();
                        foreach (var l in s.Linjer)
                        {
                            linjeListe.Add(new LinjeBasicAdmin()
                            {
                                id = l.Id,
                                Navn = l.Navn
                            });
                        }

                        stasjoners.Add(new StasjonAdmin()
                        {
                            id = s.Id,
                            Navn = s.Navn,
                            LinjeListe = linjeListe
                        });
                    }


                    return stasjoners;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                     return null;
                }

            }
        }

        //Vi sletter en Stasjon, ved å velge id
        public bool SlettStasjon(int id)
        {
            using (var db = new DB())
            {
                Stasjoner funnetStasjon = db.Stasjoner.Find(id);


                try
                {
                    db.Stasjoner.Remove(funnetStasjon);
                    db.SaveChanges();

                    logg.logg_til_change("Stasjonen " + funnetStasjon.Navn + " ble slettet");
                    
                    return true;
                }
                catch(Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }

        }


       
        public StasjonRegAdmin HentStasjon(int id)
        {
            using (var db = new DB())
            {
                var alleLinjer = db.Linjer.ToList();
                var dbStasjoner = db.Stasjoner.Find(id);

                try
                {
                    StasjonRegAdmin utStasjon = new StasjonRegAdmin()
                    {
                        id = dbStasjoner.Id,
                        Navn = dbStasjoner.Navn,
                        ValgteLinjer = new List<int>(),
                        AlleLinjer = new List<LinjeBasicAdmin>()
                    };

                    foreach (var s in dbStasjoner.Linjer)
                    {
                        utStasjon.ValgteLinjer.Add(s.Id);
                    }

                    foreach (var l in alleLinjer)
                    {
                        utStasjon.AlleLinjer.Add(new LinjeBasicAdmin()
                        {
                            id = l.Id,
                            Navn = l.Navn
                        });
                    }

                    return utStasjon;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return null;
                }
            }
        }

        public StasjonRegAdmin HentRegStasjon()
        {
            using (var db = new DB())
            {
                List<Linjer> linjeListe = db.Linjer.ToList();
                IList<LinjeBasicAdmin> alleLinjer = new List<LinjeBasicAdmin>();

                try
                {
                    foreach (var l in linjeListe)
                    {
                        alleLinjer.Add(new LinjeBasicAdmin()
                        {
                            id = l.Id,
                            Navn = l.Navn
                        });
                    }

                    StasjonRegAdmin utStasjon = new StasjonRegAdmin()
                    {
                        ValgteLinjer = new List<int>(),
                        AlleLinjer = alleLinjer
                    };

                    return utStasjon;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return null;
                }
            }
        }

       public bool EndreStasjon(StasjonRegAdmin innStasjon)
        {

            using (var db = new DB())
            {
                Stasjoner finnesStasjon = db.Stasjoner.FirstOrDefault(A => A.Navn == innStasjon.Navn);
                Stasjoner stasjon = db.Stasjoner.Find(innStasjon.id);
                List<Linjer> alleLinjer = db.Linjer.ToList();

                var liste = new List<Linjer>();

                string gammeltnavn;

                try
                {
                    if (finnesStasjon == null || !string.IsNullOrEmpty(finnesStasjon.Navn = innStasjon.Navn))
                    {
                        gammeltnavn = stasjon.Navn;
                        stasjon.Navn = innStasjon.Navn;
                        stasjon.Linjer = new List<Linjer>();
                        if (innStasjon.ValgteLinjer != null)
                        {
                            foreach (var l in alleLinjer)
                            {
                                l.Stasjoner.Remove(stasjon);
                            }

                            foreach (var l in innStasjon.ValgteLinjer)
                            {
                                var linje = db.Linjer.Find(l);
                                stasjon.Linjer.Add(linje);
                            }
                        }


                        db.SaveChanges();

                        logg.logg_til_change("Stasjonen " +gammeltnavn+" ble endret til "+stasjon.Navn);



                        return true;
                    }
                    return false;
                }

                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }
        }



        //Vi registrer en ny Stasjon
        public bool RegistrerStasjon(StasjonRegAdmin InnStasjon)
        {
            using (var db = new DB())
            {
                Stasjoner finnesStasjon = db.Stasjoner.FirstOrDefault(A => A.Navn == InnStasjon.Navn);

                try
                {
                    if (finnesStasjon == null)
                    {


                        Stasjoner nyStasjon = new Stasjoner()
                        {
                            Navn = InnStasjon.Navn,
                            Linjer = new List<Linjer>()
                        };

                        if (InnStasjon.ValgteLinjer != null)
                        {
                            foreach (var valgt in InnStasjon.ValgteLinjer)
                            {
                                var linje = db.Linjer.Find(valgt);
                                nyStasjon.Linjer.Add(linje);

                            }
                        }

                        db.Stasjoner.Add(nyStasjon);
                        db.SaveChanges();

                        string loggTekst = "Ny Stasjon: " + InnStasjon.Navn + " ble opprettet med linjene: ";

                        for (int i = 0; i < nyStasjon.Linjer.Count; i++)
                        {

                            loggTekst += nyStasjon.Linjer[i].Navn + " - ";

                        }


                        logg.logg_til_change(loggTekst);



                        return true;

                    }
                    return false;
                }

                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }

            }

        }





        //SLUTT PÅ STASJONER-METODER



        //LINJER -METODER

        public List<LinjeAdmin> ListLinjer()
        {
            using (var db = new DB())
            {
                List<Linjer> dbLinjer = db.Linjer.ToList();

                List<LinjeAdmin> alleLinjer = new List<LinjeAdmin>();

                try
                {
                    foreach (var l in dbLinjer)
                    {
                        var lDomene = new LinjeAdmin()
                        {
                            id = l.Id,
                            Navn = l.Navn,
                            Pris = l.Pris,
                            Tid = l.Tid,
                            Stasjoner = new List<string>()
                        };

                        foreach (var s in l.Stasjoner)
                        {
                            lDomene.Stasjoner.Add(s.Navn);
                        }

                        alleLinjer.Add(lDomene);
                    }
                    return alleLinjer;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return null;
                }

            }
        }

        public LinjeAdmin HentLinje(int id)
        {
            using (var db = new DB())
            {
                try
                {
                    Linjer dbLinje = db.Linjer.Find(id);


                    LinjeAdmin utLinje = new LinjeAdmin()
                    {
                        id = dbLinje.Id,
                        Navn = dbLinje.Navn,
                        Pris = dbLinje.Pris,
                        Tid = dbLinje.Tid,
                        Stasjoner = new List<string>()
                    };

                    return utLinje;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return null;
                }
            }
        }

        public LinjeRegAdmin HentRegLinje()
        {
            using (var db = new DB())
            {
                try
                {


                    List<Stasjoner> stasjonListe = db.Stasjoner.ToList();
                    IList<StasjonAdmin> alleStasjoner = new List<StasjonAdmin>();
                    foreach (var s in stasjonListe)
                    {

                        alleStasjoner.Add(new StasjonAdmin()
                        {
                            id = s.Id,
                            Navn = s.Navn
                        });
                    }

                    LinjeRegAdmin utLinje = new LinjeRegAdmin()
                    {
                        AlleStasjoner = alleStasjoner,
                        ValgteStasjoner = new List<int>()

                    };

                    return utLinje;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return null;
                }
            }
        }

        public bool EndreLinje(LinjeAdmin innLinje)
        {
            using (var db = new DB())
            {
                try
                {
               
                    Linjer funnetLinjer = db.Linjer.FirstOrDefault(L => L.Navn == innLinje.Navn);

                    Linjer linje = db.Linjer.Find(innLinje.id);

                    string gammellinje;
                    string gammelpris;
                    string gammeltid;
                    if (funnetLinjer == null || !string.IsNullOrEmpty(funnetLinjer.Navn = innLinje.Navn))
                    {
                        gammellinje = linje.Navn;
                        gammelpris = linje.Pris.ToString();
                        gammeltid = linje.Tid.ToString();
                        linje.Navn = innLinje.Navn;
                        linje.Pris = innLinje.Pris;
                        linje.Tid = innLinje.Tid;

                        db.SaveChanges();

                        
                        logg.logg_til_change("Linjen " + gammellinje + " ble endret til " + innLinje.Navn + " og  tiden "+gammeltid+" ble endret til "+innLinje.Tid+" og Prisen "+gammelpris+" ble endret til "+linje.Pris);


                        return true;
                    }

                    return false;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }
        }

        public bool DeleteLinje(int id)
        {
            using (var db = new DB())
            {
                try
                {
                    var linje = db.Linjer.Find(id);
                    db.Linjer.Remove(linje);
                    db.SaveChanges();

                    logg.logg_til_change("Linjen "+linje.Navn+"  ble slettet");

                    return true;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;

                }
            }
        }

       


        public bool RegistrerLinje(LinjeRegAdmin nyLinje)
        {
            using (var db = new DB())
            {

                try
                {
                    Linjer funnetLinjer = db.Linjer.FirstOrDefault(L => L.Navn == nyLinje.Navn);

                    if (funnetLinjer == null)
                    {

                        Linjer linje = new Linjer()
                        {
                            Navn = nyLinje.Navn,
                            Pris = nyLinje.Pris,
                            Tid = nyLinje.Tid,
                            Stasjoner = new List<Stasjoner>()
                        };
                        if (nyLinje.ValgteStasjoner != null)
                        {
                            foreach (var s in nyLinje.ValgteStasjoner)
                            {
                                var stasjon = db.Stasjoner.Find(s);
                                linje.Stasjoner.Add(stasjon);
                            }
                        }

                        db.Linjer.Add(linje);
                        db.SaveChanges();

                        string loggTekst = "Ny linje: " + linje.Navn + " og pris " + linje.Pris + " og tid " + linje.Tid + " ble opprettet med Stasjonene: ";

                        for (int i = 0; i < linje.Stasjoner.Count; i++)
                        {

                            loggTekst += linje.Stasjoner[i].Navn + " - ";

                        }

                        logg.logg_til_change(loggTekst);


                        return true;
                    }

                    return false;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }
        }


        //KUNDER
        public List<Kunder> ListKunder()
        {
            try
            {
                using (var db = new DB())
                {
                    return db.Kunder.ToList();
                }
            }
            catch(Exception e)
            {
                logg.logg_til_exception(e.ToString());
                return null;
            }
        }

        public KundeAdmin HentKunde(int id)
        {
            try
            {
                using (var db = new DB())
                {
                    Kunder kunder = db.Kunder.Find(id);


                    KundeAdmin utKunde = new KundeAdmin()
                    {
                        id = kunder.Id,
                        Telefon = kunder.Telefon,
                        Epost = kunder.Epost
                    };

                    return utKunde;
                }
            }
            catch (Exception e)
            {
                logg.logg_til_exception(e.ToString());

                return null;
            }
        }

        public bool EndreKunde(KundeAdmin innKunde)
        {  
            try
            {
                using (var db = new DB())
                {
                   
                    Kunder funnetKunde = db.Kunder.Find(innKunde.id);
                    string gammelTlf = funnetKunde.Telefon;
                    string gammelEpost = funnetKunde.Epost;

                    funnetKunde.Telefon = innKunde.Telefon;
                    funnetKunde.Epost = innKunde.Epost;

                    
                    db.SaveChanges();


                        logg.logg_til_change("Kunden ble oppdatert fra: " + gammelTlf + " - " + gammelEpost + " TIL " + innKunde.Telefon + " - " + innKunde.Epost); 


                            


                   
                
                    return true;


                }
               
            }
            catch (Exception e)
            {
                logg.logg_til_exception(e.ToString());
                return false;
            }
        }

        public bool DeleteKunde(int id)
        {
            using (var db = new DB())
            {
                try
                {
                    var kunde = db.Kunder.Find(id);
                    db.Kunder.Remove(kunde);
                    db.SaveChanges();

                    logg.logg_til_change(" Kunde ble slettet :" + kunde.Epost);
                    return true;
                }
                catch (Exception e)
                {
                    logg.logg_til_exception(e.ToString());
                    return false;
                }
            }
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
