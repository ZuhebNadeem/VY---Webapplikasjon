using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using DAL;
using Model;
using VY.Models;

namespace VY.Controllers
{
    public class HomeController : Controller
    {
        // DB
        VyBLL vyBLL = new VyBLL();


        //Hjemmeside - Billett bestilling
        public ActionResult Index()
        {
            return View();
        }


        //sender serialisert stasjoner liste med json
        public string hentAlleStasjoner()
        {
            var db = new DBLogikk();
            return db.jsonAlleStasjoner();
        }


        //Ajax oppsett for henting av avganger
        public string hentRute(string Dato, string Tid, string FraStasjon, string TilStasjon, string Linje, string AntVoksen, string AntBarn, string TurRetur, string ReturDato, string ReturTid)
        {
            var db = new DBLogikk();
            var ut = db.hentRute(Dato, Tid, FraStasjon, TilStasjon, Linje, AntVoksen,AntBarn, TurRetur, ReturDato, ReturTid);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(ut);
            return json;
        }

        //ordrePartialview
        public ActionResult HentOrdre(string Dato, string Tid, string FraStasjon, string TilStasjon, string AntVoksne,
            string AntBarn, string TurRetur, string ReturDato, string ReturTid)
        {
            var db = new DBLogikk();
            var ut = db.HentOrdre(Dato, Tid, FraStasjon, TilStasjon, AntVoksne, AntBarn, TurRetur, ReturDato,ReturTid);
            return PartialView("~/Views/PartialView/SeAvganger.cshtml", ut);
        }

        public string RegistrerOrdre(Bestilling bestilling)
        {
            var db = new DBLogikk();
            var ut = db.lagreOrdre(bestilling);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(ut);

        }

        //Avganger partialview
        [HttpGet]
        public ActionResult AvgangerPartialView()
        {
            return PartialView("~/Views/PartialView/SeAvganger.cshtml");
        }


        //Linjekart og stasjoner
        public ActionResult Linjekart()
        {
            return View();
        }


        //Stasjoner partialview
        [HttpGet]
        public ActionResult StasjonerPartialView()
        {
            DB db = new DB();
            var alleStasjoner= db.Stasjoner.ToList();
            return PartialView("/Views/PartialView/SeStasjoner.cshtml", alleStasjoner);
        }


        //Takk for bestilling
        public ActionResult Goodbye()
        {
            return View();
        }


        //ADMIN LOGG INN
        public ActionResult Logginn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logginn(Admin innAdmin)
        {
            Debug.Print("Inne i metoden");

            if (vyBLL.finnesAdmin(innAdmin))
            {
                Debug.Print("Bruker og passord finnes");
                Session["LoggetInn"] = true;
                return RedirectToAction("Index", "Admin");

            }
            
                Debug.Print("Finnes ikke");
                Session["LoggetInn"] = false;
                return View();

        }



    }
}