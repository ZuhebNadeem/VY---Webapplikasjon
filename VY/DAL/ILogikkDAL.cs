using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public interface ILogikkDAL
    {



        //ADMIN-METODER
        bool finnesAdmin(Admin InnAdmin);


        bool Registrer(Admin InnAdmin);

        void RegistrerSuper();

        Admin HentAdmin(int id);

        List<Adminer> ListAdminer();

        bool EndreAdmin(int id, Admin InnAdmin);

        bool SlettAdmin(int id);
       

        //STASJONER - METODER
        List<StasjonAdmin> ListStasjoner();

        bool SlettStasjon(int id);

        StasjonRegAdmin HentStasjon(int id);

        StasjonRegAdmin HentRegStasjon();

        bool EndreStasjon(StasjonRegAdmin innStasjon);

        bool RegistrerStasjon(StasjonRegAdmin InnStasjon);
        


        //LINJER -METODER

        List<LinjeAdmin> ListLinjer();

        LinjeAdmin HentLinje(int id);

        LinjeRegAdmin HentRegLinje();

        bool EndreLinje(LinjeAdmin innLinje);

        bool DeleteLinje(int id);

        bool RegistrerLinje(LinjeRegAdmin nyLinje);

        List<Kunder> ListKunder();
        KundeAdmin HentKunde(int id);
        bool EndreKunde(KundeAdmin innKunde);
        bool DeleteKunde(int id);
    }
}
