using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class VyBLL : IVyBLL
    {
        private ILogikkDAL _logikkDal;

        public VyBLL()
        {
            _logikkDal = new LogikkDAL();
        }

        public VyBLL(ILogikkDAL stub)
        {
            _logikkDal = stub;
        }



        //ADMIN-METODER
        public bool finnesAdmin(Admin InnBruker)
        {
            return _logikkDal.finnesAdmin(InnBruker);
        }

        public bool Registrer(Admin InnAdmin)
        {
            return _logikkDal.Registrer(InnAdmin);
        }

        public List<Adminer> ListAdminer()
        {
            List<Adminer> alleAdminer = _logikkDal.ListAdminer();
            return alleAdminer;
        }

        public Admin HentAdmin(int id)
        {
            return _logikkDal.HentAdmin(id);
        }


        public bool EndreAdmin(int id, Admin InnAdmin)
        {
            return _logikkDal.EndreAdmin(id,InnAdmin);
        }


        public bool SlettAdmin(int id)
        {
            return _logikkDal.SlettAdmin(id);
        }

        //SLUTT PÅ ADMIN-METODER




        //STASJONER-METODER
        public List<StasjonAdmin> ListStasjoner()
        {
            List<StasjonAdmin> stasjoners = _logikkDal.ListStasjoner();
            return stasjoners;
        }

        public bool SlettStasjon(int id)
        {
            return _logikkDal.SlettStasjon(id);
        }

        public bool RegistrerStasjon(StasjonRegAdmin InnStasjon)
        {
            return _logikkDal.RegistrerStasjon(InnStasjon);
        }

        public StasjonRegAdmin HentStasjon(int id)
        {
            return _logikkDal.HentStasjon(id);
        }
        public StasjonRegAdmin HentRegStasjon()
        {
            return _logikkDal.HentRegStasjon();
        }

        public bool EndreStasjon(StasjonRegAdmin innStasjon)
        {
            return _logikkDal.EndreStasjon(innStasjon);
        }


        //SLUTT PÅ STASJONER-METODER





        //LINJER-metoder
        public List<LinjeAdmin> ListLinjer()
        {
            return _logikkDal.ListLinjer();
        }

        public LinjeAdmin HentLinje(int id)
        {
            return _logikkDal.HentLinje(id);
        }
        public LinjeRegAdmin HentRegLinje()
        {
            return _logikkDal.HentRegLinje();
        }

        public bool EndreLinje(LinjeAdmin innLinje)
        {
            return _logikkDal.EndreLinje(innLinje);
        }

        public void DeleteLinje(int id)
        {
            _logikkDal.DeleteLinje(id);
        }
        public bool RegistrerLinje(LinjeRegAdmin nyLinje)
        {
          return _logikkDal.RegistrerLinje(nyLinje);
        }

        public List<Kunder> ListKunder()
        {
            return _logikkDal.ListKunder();
        }

        public KundeAdmin HentKunde(int id)
        {
            return _logikkDal.HentKunde(id);
            throw new NotImplementedException();
        }

        public bool EndreKunde(KundeAdmin innKunde)
        {
            return _logikkDal.EndreKunde(innKunde);
            throw new NotImplementedException();
        }

        public bool DeleteKunde(int id)
        {
            return _logikkDal.DeleteKunde(id);
        }
    }
}
