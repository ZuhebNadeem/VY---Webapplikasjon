using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using Model;
using VY.Models.Domene;
using System;
using System.IO;

namespace VY.Controllers
{
    

    public class AdminController : Controller
    {
        private IVyBLL _VyBLL;

        public AdminController()
        {
            _VyBLL = new VyBLL();
        }

        public AdminController(IVyBLL stub)
        {
            _VyBLL = stub;
        }

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            return View();


        }

        //ADMIN-METODER
        public ActionResult RegistrerAdmin()
        {

            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            return View();


        }

        [HttpPost]
        public ActionResult RegistrerAdmin(Admin innAdmin)
        {
            if (ModelState.IsValid)
            {
                if (_VyBLL.Registrer(innAdmin))
                {
                    Session["RegistrerAdmin"] = true;
                    return RedirectToAction("ListAdmin", "Admin");
                }
                Session["RegistrerAdmin"] = false;
                return View();
            }
            Session["RegistrerAdmin"] = true;

            return View();
        }


        //Liste ut alle adminer som er i databasen
        public ActionResult ListAdmin()
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }


            var alleAdminer = _VyBLL.ListAdminer();
            return View(alleAdminer);

        }


        //Endre adminer i databasen
        public ActionResult EditAdmin(int id)
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            Admin admin = _VyBLL.HentAdmin(id);
            return View(admin);

        }


        //Endre adminer i databasen
        [HttpPost]
        public ActionResult EditAdmin(int id, Admin InnAdmin)
        {
            if (ModelState.IsValidField("Brukernavn"))
            {
                if (_VyBLL.EndreAdmin(id, InnAdmin)) //om Id er riktig retunerer du til list side og endringen blir utført
                {
                    Session["EndreAdmin"] = true;
                    return RedirectToAction("ListAdmin", "Admin");
                }
                Session["EndreAdmin"] = false;
                    return View();
            }
            
            Session["EndreAdmin"] = true;
            return View();
            
           
        }



        //Slett alle adminer som er i databasen
        public ActionResult DeleteAdmin(int id)
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }
            
            _VyBLL.SlettAdmin(id);
            return RedirectToAction("ListAdmin", "Admin");
        }

        //SLUTT PÅ ADMIN-METODER



        //STASJONER-METODER

        public ActionResult RegistrerStasjon()
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            StasjonRegAdmin nyStasjon = _VyBLL.HentRegStasjon();
            return View(nyStasjon);


        }

        [HttpPost]
        public ActionResult RegistrerStasjon(StasjonRegAdmin innStasjon)
        {
            StasjonRegAdmin nyStasjon;
            if (ModelState.IsValid)
            {
                if(_VyBLL.RegistrerStasjon(innStasjon))
                {
                    Session["RegistrerStasjon"] = true;
                    return RedirectToAction("ListStasjoner", "Admin");
                }
                Session["RegistrerStasjon"] = false;
                nyStasjon = _VyBLL.HentRegStasjon();
                return View(nyStasjon);
            }
            Session["RegistrerStasjon"] = true;
            nyStasjon = _VyBLL.HentRegStasjon();
            return View(nyStasjon);


        }



        //Vi lister ut alle stasjoner i et view
        public ActionResult ListStasjoner()
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            List<StasjonAdmin> stasjoners = _VyBLL.ListStasjoner();
            return View(stasjoners);
        }

        //Endre en stasjon
        public ActionResult EndreStasjon(int id)
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            StasjonRegAdmin stasjon = _VyBLL.HentStasjon(id);
            return View(stasjon);
        }

        [HttpPost]
        public ActionResult EndreStasjon(StasjonRegAdmin innStasjon,int id)
        {
          
            StasjonRegAdmin stasjon;
            if (ModelState.IsValid)
            {
                if (_VyBLL.EndreStasjon(innStasjon))
                {
                    Session["EndreStasjon"] = true;
                    return RedirectToAction("ListStasjoner", "Admin");
                }
                Session["EndreStasjon"] = false;
                stasjon = _VyBLL.HentStasjon(id);
                return View(stasjon);

            }
            Session["EndreStasjon"] = true;
            stasjon = _VyBLL.HentStasjon(id);
            return View(stasjon);

        }


        //Slette en stasjon
        public ActionResult SletteStasjon(int id)
        {
            if (Session["LoggetInn"] == null || (bool) Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }


            _VyBLL.SlettStasjon(id);
            return RedirectToAction("ListStasjoner", "Admin"); 
        }



        // SLUTT PÅ STASJONER-METODER






        //Linjer

        //LINJER-METODER
        public ActionResult ListLinjer()
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            var alleLinjer = _VyBLL.ListLinjer();
            return View(alleLinjer);
        }

        public ActionResult EditLinje(int id)
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            LinjeAdmin linje = _VyBLL.HentLinje(id);
            return View(linje);

        }

        [HttpPost]
        public ActionResult EditLinje(LinjeAdmin innLinje, int id)
        {
            LinjeAdmin linje;
            if (ModelState.IsValid)
            {
                if (_VyBLL.EndreLinje(innLinje))
                {
                    Session["EditLinjen"] = true;
                    return RedirectToAction("ListLinjer", "Admin");
                }
                Session["EditLinjen"] = false;
                linje = _VyBLL.HentLinje(id);
                return View(linje);
            }
            Session["EditLinjen"] = true;
            linje = _VyBLL.HentLinje(id);
            return View(linje);

        }

        public ActionResult DeleteLinje(int id)
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            _VyBLL.DeleteLinje(id);
            return RedirectToAction("ListLinjer", "Admin");

        }

        public ActionResult RegistrerLinje()
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            LinjeRegAdmin linjeRegAdmin = _VyBLL.HentRegLinje();
            return View(linjeRegAdmin);
        }

        [HttpPost]
        public ActionResult RegistrerLinje(LinjeRegAdmin nyLinje)
        {
            
            LinjeRegAdmin linjeRegAdmin;
            if (ModelState.IsValid)
            {
                if (_VyBLL.RegistrerLinje(nyLinje))
                {
                    Session["RegistLinje"] = true;
                    return RedirectToAction("ListLinjer", "Admin");
                }
                Session["RegistLinje"] = false;
                linjeRegAdmin = _VyBLL.HentRegLinje();
                return View(linjeRegAdmin);
            }
            Session["RegistLinje"] = true;
            linjeRegAdmin = _VyBLL.HentRegLinje();
            return View(linjeRegAdmin);
        }


        //KUNDER
        public ActionResult ListKunder()
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            var alleKunder = _VyBLL.ListKunder();
            return View(alleKunder);
        }

        public ActionResult EditKunder(int id)
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            KundeAdmin kunde = _VyBLL.HentKunde(id);
            return View(kunde);

        }

        [HttpPost] 
        public ActionResult EditKunder(KundeAdmin innKunde) 
        {

            if (ModelState.IsValid)
            {
                _VyBLL.EndreKunde(innKunde);
                return RedirectToAction("ListKunder", "Admin");

            }
            return View();
        }



        public ActionResult DeleteKunde(int id)
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }

            _VyBLL.DeleteKunde(id);
            return RedirectToAction("ListKunder", "Admin");

        }






        //NAVBAR
        public ActionResult AdminView()
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }
            return View();
        }

        public ActionResult VedlikeholdView()
        {
            if (Session["LoggetInn"] == null || (bool)Session["LoggetInn"] == false)
            {
                return RedirectToAction("Logginn", "Home");
            }
            return View();
        }

        public ActionResult Loggut()
        {
            Session["LoggetInn"] = false;
            return RedirectToAction("Logginn", "Home");
        }






    }
}