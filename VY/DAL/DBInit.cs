using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DAL
{
    public class DBInit : DropCreateDatabaseAlways<DB> 
    {
        protected override void Seed(DB context)
        {

            //BYER
            var Drammen = new Stasjoner
            {
                Navn = "Drammen"
            };
            var Asker = new Stasjoner
            {
                Navn = "Asker"
            };
            
            var Oslo = new Stasjoner
            {
                Navn = "Oslo"
            };
            var Lillestrom = new Stasjoner
            {
                Navn = "Lillestrøm"
            };
            var Gardermoen = new Stasjoner
            {
                Navn = "Gardermoen"
            };
            var Mysen = new Stasjoner
            {
                Navn = "Mysen"
            };
            var Ski = new Stasjoner
            {
                Navn = "Ski"
            };
            var Jaren = new Stasjoner
            {
                Navn = "Jaren"
            };


            //LINJER
            var L1 = new Linjer
            {
                Navn = "L1",
                Pris = 50,
                Tid = 12
            };

            var L2 = new Linjer
            {
                Navn = "L2",
                Pris = 70,
                Tid = 16
            };

            var L3 = new Linjer
            {
                Navn = "L3",
                Pris = 70,
                Tid = 16
            };


            //Linjer stasjoner
            var L1stasjoner = new List<Stasjoner>();
            L1stasjoner.Add(Drammen);
            L1stasjoner.Add(Asker);
            L1stasjoner.Add(Oslo);
            L1stasjoner.Add(Jaren);

            var L2stasjoner = new List<Stasjoner>();
            L2stasjoner.Add(Mysen);
            L2stasjoner.Add(Ski);
            L2stasjoner.Add(Oslo);
            L2stasjoner.Add(Lillestrom);
            L2stasjoner.Add(Gardermoen);


            L1.Stasjoner = L1stasjoner;
            L2.Stasjoner = L2stasjoner;



            //Linje per stasjon
            var ListDrammen = new List<Linjer>();
            ListDrammen.Add(L1);

            var ListAsker = new List<Linjer>();
            ListAsker.Add(L1);

            var ListJaren = new List<Linjer>();
            ListJaren.Add(L1);


            var ListLillestrom = new List<Linjer>();
            ListLillestrom.Add(L2);

            var ListGardermoen = new List<Linjer>();
            ListGardermoen.Add(L2);


            var ListMysen = new List<Linjer>();
            ListMysen.Add(L2);

            var ListSki = new List<Linjer>();
            ListSki.Add(L2);

            

            var ListOslo = new List<Linjer>();
            ListOslo.Add(L1);
            ListOslo.Add(L2);


            Drammen.Linjer = ListDrammen;
            Asker.Linjer = ListAsker;
            Lillestrom.Linjer = ListLillestrom;
            Gardermoen.Linjer = ListGardermoen;

            Oslo.Linjer = ListOslo;

            Mysen.Linjer = ListMysen;
            Ski.Linjer = ListSki;
            Jaren.Linjer = ListJaren;



            var nyRute1 = new Ruter
            {
                FraStasjon = Drammen.Navn,
                TilStasjon = Oslo.Navn,
                Dato = DateTime.Now
            };


            var nyRute2 = new Ruter
            {
                FraStasjon = Oslo.Navn,
                TilStasjon = Jaren.Navn,
                Dato = DateTime.Now
            };

            var nyOrdre = new Ordrer
            {
                Sum = 150,
                Ruter = new List<Ruter>()
            };

            nyOrdre.Ruter.Add(nyRute1);
            nyOrdre.Ruter.Add(nyRute2);

    
            var nyKunde = new Kunder()
            {
                Telefon = "99887766",
                Epost = "test@hotmail.no",
                BetalingsMetode = "Vipps"
            };

            var nyKunde2 = new Kunder()
            {
                Telefon = "22224444",
                Epost = "test2@hotmail.no",
                BetalingsMetode = "Vipps"
            };






            context.Linjer.Add(L1);
            context.Linjer.Add(L2);
            context.Linjer.Add(L3);

            context.Stasjoner.Add(Drammen);
            context.Stasjoner.Add(Asker);
            context.Stasjoner.Add(Oslo);
            context.Stasjoner.Add(Lillestrom);
            context.Stasjoner.Add(Gardermoen);
            context.Stasjoner.Add(Jaren);
            context.Stasjoner.Add(Mysen);
            context.Stasjoner.Add(Ski);

            context.Ruter.Add(nyRute1);
            context.Ruter.Add(nyRute2);

            context.Ordrer.Add(nyOrdre);

            context.Kunder.Add(nyKunde);
            context.Kunder.Add(nyKunde2);


            //ADMIN
            LogikkDAL logikk = new LogikkDAL();
            logikk.RegistrerSuper();

            base.Seed(context);


        }
    }
}