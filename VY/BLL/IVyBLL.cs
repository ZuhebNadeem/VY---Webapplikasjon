using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public interface IVyBLL
    {
        //ADMIN-METODER

        bool finnesAdmin(Admin InnBruker);


        bool Registrer(Admin InnAdmin);


        List<Adminer> ListAdminer();


        Admin HentAdmin(int id);



        bool EndreAdmin(int id, Admin InnAdmin);



        bool SlettAdmin(int id);
        

        //STASJONER-METODER
        List<StasjonAdmin> ListStasjoner();


        bool SlettStasjon(int id);


        bool RegistrerStasjon(StasjonRegAdmin InnStasjon);


        StasjonRegAdmin HentStasjon(int id);

        StasjonRegAdmin HentRegStasjon();


        bool EndreStasjon(StasjonRegAdmin innStasjon);
        


        //LINJER-metoder
        List<LinjeAdmin> ListLinjer();


        LinjeAdmin HentLinje(int id);

        LinjeRegAdmin HentRegLinje();

        bool EndreLinje(LinjeAdmin innLinje);

        void DeleteLinje(int id);
        bool RegistrerLinje(LinjeRegAdmin nyLinje);

        List<Kunder> ListKunder();
        KundeAdmin HentKunde(int id);
        bool EndreKunde(KundeAdmin innKunde);
        bool DeleteKunde(int id);
    }
}
