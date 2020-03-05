using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public class Stasjoner
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public virtual List<Linjer> Linjer { get; set; }
    }

    public class Linjer
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int Pris { get; set; }
        public int Tid { get; set; }
        public virtual List<Stasjoner> Stasjoner { get; set; }
    }

    public class Ruter
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public string FraStasjon { get; set; }
        public string  TilStasjon { get; set; }
        

    }

    public class Ordrer
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public virtual List<Ruter> Ruter { get; set; }

    }

    public class Kunder
    {
        public int Id { get; set; }
        public string Telefon { get; set; }
        public string Epost { get; set; }
        public string BetalingsMetode { get; set; }
        //public virtual List<Ordrer> Ordrer { get; set; }

    }

    //ADMIN BRUKERE 
    public class Adminer
    {

        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public string Salt { get; set; }
    }





    public class DB: DbContext
    {
        public DB() : base("name=DB")
        {
            Database.CreateIfNotExists();

            //En DBinit som setter verdier ved start
            Database.SetInitializer<DB>(new DBInit());
        }


        //For å ikke få s'er
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Stasjoner> Stasjoner { get; set; }

        public virtual DbSet<Linjer> Linjer { get; set; }
        public virtual DbSet<Ruter> Ruter { get; set; }
        public virtual DbSet<Ordrer> Ordrer { get; set; }
        public virtual DbSet<Kunder> Kunder { get; set; }
        public virtual DbSet<Adminer> Adminer { get; set; }



    }
}