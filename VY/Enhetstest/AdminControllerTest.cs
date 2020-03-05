using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using System.Web.Mvc;
using VY.Controllers;
using BLL;
using DAL;
using System.Collections.Generic;
using System.Linq;
using Model;


namespace Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {

        //INDEX
        [TestMethod]
        public void Index_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController();

            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult) controller.Index();

            //assert

            Assert.AreEqual(actionResult.RouteName,"");
            Assert.AreEqual(actionResult.RouteValues.Values.First(),"Logginn");

        }

        [TestMethod]
        public void Index_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController();

            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.Index();

            //assert

            Assert.AreEqual(actionResult.ViewName, "");

        }

        //REGISTRER ADMIN
        [TestMethod]
        public void RegistrerAdmin_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController();

            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.RegistrerAdmin();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void RegistrerAdmin_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.RegistrerAdmin();

            //assert

            Assert.AreEqual(actionResult.ViewName, "");

        }


        [TestMethod]
        public void RegistrerAdmin_Validering_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerAdmin"] = null;


            var innAdmin = new Admin();

            controller.ViewData.ModelState.AddModelError("Brukernavn","brukernavn er ikke oppgitt");

            //act
            var resultat = (ViewResult)controller.RegistrerAdmin(innAdmin);

            //assert
            Assert.IsTrue(resultat.ViewData.ModelState.Count == 1);
            Assert.AreEqual(resultat.ViewName, "");

        }

        [TestMethod]
        public void RegistrerAdmin_Post_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerAdmin"] = null;

            var innAdmin = new Admin()
            {
                Brukernavn = ""
            };
            
            //act
            var resultat = (ViewResult)controller.RegistrerAdmin(innAdmin);

            //assert
            Assert.AreEqual(resultat.ViewName, "");

        }

        [TestMethod]
        public void RegistrerAdmin_Post_True()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerAdmin"] = null;

            var innAdmin = new Admin()
            {
                Brukernavn = "test"
            };

            //act
            var resultat = (RedirectToRouteResult)controller.RegistrerAdmin(innAdmin);

            //assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(),"ListAdmin");

        }


        //ListAdmin
        [TestMethod]
        public void ListAdmin_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.ListAdmin();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void ListAdmin_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
             controller.Session["LoggetInn"] = true;


            var forventetResultat = new List<Adminer>();
            var admin = new Adminer()
            {
                Id = 2,
                Brukernavn = "test",
                Passord = new byte[] {},
                Salt = "t"
            };

            forventetResultat.Add(admin);
            forventetResultat.Add(admin);
            forventetResultat.Add(admin);

            //act
            var actionResult = (ViewResult)controller.ListAdmin();
            var resultat = (List<Adminer>) actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (int i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Id,resultat[i].Id);
                Assert.AreEqual(forventetResultat[i].Brukernavn, resultat[i].Brukernavn);
                Assert.AreEqual(forventetResultat[i].Salt, resultat[i].Salt);
            }
        }


        //EDITADMIN
        [TestMethod]
        public void EditAdmin_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.EditAdmin(1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }

        [TestMethod]
        public void EditAdmin_Edit_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EditAdmin(0);
            var adminResult = (Admin) actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(adminResult.id,0);
        }

        [TestMethod]
        public void EditAdmin_Edit_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EditAdmin(1);
            
            //assert

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditAdmin_Validering_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreAdmin"] = null;


            var innAdmin = new Admin();

            controller.ViewData.ModelState.AddModelError("Brukernavn", "brukernavn er ikke oppgitt");

            //act
            var resultat = (ViewResult) controller.EditAdmin(0, innAdmin);

            //assert
            Assert.IsTrue(resultat.ViewData.ModelState.Count == 1);
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void EditAdmin_Post_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreAdmin"] = true;

            var innAdmin = new Admin()
            {
                id = 0,
                Brukernavn = "test",
                Passord = "testPass"
            };

            //act
            var actionResult = (ViewResult)controller.EditAdmin(0,innAdmin);

            //assert

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditAdmin_Post_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreAdmin"] = true;

            var innAdmin = new Admin()
            {
                id = 1,
                Brukernavn = "test",
                Passord = "testPass"
            };

            //act
            var actionResult = (RedirectToRouteResult)controller.EditAdmin(1, innAdmin);

            //assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListAdmin");
        }



        //DELETE ADMIN
        [TestMethod]
        public void DeleteAdmin_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteAdmin(1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }

        [TestMethod]
        public void DeleteAdmin_Delete_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteAdmin(2);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListAdmin");

        }


        //STASJONER

        // RegistrerStasjon
        [TestMethod]
        public void Registrerstasjon_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;



            //act
            var actionResult = (RedirectToRouteResult)controller.RegistrerStasjon();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }

        [TestMethod]
        public void Registrerstasjon_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            StasjonRegAdmin nyregAdmin = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "test",
                AlleLinjer = new List<LinjeBasicAdmin>() ,
                ValgteLinjer = new List<int>()


               
            };



            //act

            //act
            var actionResult = (ViewResult)controller.RegistrerStasjon();
            var resultat = (StasjonRegAdmin)actionResult.Model;


            
            //assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(nyregAdmin.id, resultat.id);
            Assert.AreEqual(nyregAdmin.Navn, resultat.Navn);
            Assert.AreEqual(nyregAdmin.AlleLinjer.Count, resultat.AlleLinjer.Count);
            Assert.AreEqual(nyregAdmin.ValgteLinjer.Count, resultat.ValgteLinjer.Count);


        }




        [TestMethod]
        public void RegistrerStasjon_Validering_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerStasjon"] = null;

            var innStasjon = new StasjonRegAdmin();
           




            controller.ViewData.ModelState.AddModelError("Navn", "Stasjons sitt navn, kan ikke være tom og må være kun bokstaver");

            //act
            var actionResult = (ViewResult)controller.RegistrerStasjon(innStasjon);
            var resultat = (StasjonRegAdmin)actionResult.Model;

            //assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
           
            Assert.AreEqual(actionResult.ViewName, "");

        }


        [TestMethod]
        public void RegistrerStasjon_Post_True()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerStasjon"] = null;

            var innStasjon = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "test",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()

            };


            //act
            var actionResult = (RedirectToRouteResult)controller.RegistrerStasjon(innStasjon);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListStasjoner");


        }

        [TestMethod]
        public void RegistrerStasjon_Post_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistrerStasjon"] = null;

            var innStasjon = new StasjonRegAdmin()
            {
                id = 0,
               

            };

            var viewStasjon = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "test",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()

            };


            //act
            var actionResult = (ViewResult)controller.RegistrerStasjon(innStasjon);
            var resultat = (StasjonRegAdmin)actionResult.Model;

            //assert
            Assert.AreEqual(viewStasjon.id, resultat.id);
            Assert.AreEqual(viewStasjon.Navn, resultat.Navn);
            Assert.AreEqual(viewStasjon.AlleLinjer.Count, resultat.AlleLinjer.Count);
            Assert.AreEqual(viewStasjon.ValgteLinjer.Count, resultat.ValgteLinjer.Count);
            Assert.AreEqual(actionResult.ViewName, "");

        }







        //ListStasjon
        [TestMethod]
        public void ListStasjoner_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.ListStasjoner();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void ListStasjoner_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;


            var forventetResultat = new List<StasjonAdmin>();
            var stasjonsadmin = new StasjonAdmin()
            {
                id = 2,
                Navn = "Oslo S",
                LinjeListe = new List<LinjeBasicAdmin>()

            };

            forventetResultat.Add(stasjonsadmin);
            forventetResultat.Add(stasjonsadmin);
            forventetResultat.Add(stasjonsadmin);

            //act
            var actionResult = (ViewResult)controller.ListStasjoner();
            var resultat = (List<StasjonAdmin>)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (int i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);
                Assert.AreEqual(forventetResultat[i].LinjeListe.Count, resultat[i].LinjeListe.Count);



            }

        }

        //Endre Stasjon 



        [TestMethod]
        public void Endrestasjon_Admin_False()
        {
            //arrange

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.EndreStasjon(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void Endrestasjon_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EndreStasjon(1);
            var StasjonResult = (StasjonRegAdmin)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(StasjonResult.id, 1);
        }


        [TestMethod]
        public void EndreStasjon_Validering_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreStasjon"] = null;

            var Stasjon1 = new StasjonRegAdmin();
            
            controller.ViewData.ModelState.AddModelError("Navn", "Stasjons sitt navn, kan ikke være tom og må være kun bokstaver");

            //act
            var actionResult = (ViewResult)controller.EndreStasjon(Stasjon1,0);
            var resultat = (StasjonRegAdmin)actionResult.Model;

            //assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");

        }


        [TestMethod]
        public void EndreStasjon_Post_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreStasjon"] = null;

            var innStasjon = new StasjonRegAdmin()
            {
                id = 0


            };

            var viewStasjon = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "Løren",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()

            };


            //act
            var actionResult = (ViewResult)controller.EndreStasjon(innStasjon,1);
            var resultat = (StasjonRegAdmin)actionResult.Model;

            //assert
            Assert.AreEqual(viewStasjon.id, resultat.id);
            Assert.AreEqual(viewStasjon.Navn, resultat.Navn);
            Assert.AreEqual(viewStasjon.AlleLinjer.Count, resultat.AlleLinjer.Count);
            Assert.AreEqual(viewStasjon.ValgteLinjer.Count, resultat.ValgteLinjer.Count);
            Assert.AreEqual(actionResult.ViewName, "");

        }




        [TestMethod]
        public void EndreStasjon_Post_True()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EndreStasjon"] = null;

            var innStasjon = new StasjonRegAdmin()
            {
                id = 1,
                Navn = "Løren",
                AlleLinjer = new List<LinjeBasicAdmin>(),
                ValgteLinjer = new List<int>()

            };


            //act
            var actionResult = (RedirectToRouteResult)controller.EndreStasjon(innStasjon,1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListStasjoner");


        }


        //slette stasjoner


        [TestMethod]
        public void DeleteStation_Station_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.SletteStasjon(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void DeleteStation_Station_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (RedirectToRouteResult)controller.SletteStasjon(2);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListStasjoner");

        }





        // lINJER

        //list 
        [TestMethod]
        public void ListLinjer_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.ListLinjer();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void ListLinjer_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;


            var forventetResultat = new List<LinjeAdmin>();
            var linjeadmin = new LinjeAdmin()
            {
                id = 2,
                Navn = "L1",
                Pris = 200,
                Stasjoner = new List<string>(),
                Tid = 2

            };

            forventetResultat.Add(linjeadmin);
            forventetResultat.Add(linjeadmin);
            forventetResultat.Add(linjeadmin);

            //act
            var actionResult = (ViewResult)controller.ListLinjer();
            var resultat = (List<LinjeAdmin>)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (int i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);
                Assert.AreEqual(forventetResultat[i].Pris, resultat[i].Pris);
                Assert.AreEqual(forventetResultat[i].Stasjoner.Count, resultat[i].Stasjoner.Count);
                Assert.AreEqual(forventetResultat[i].Tid, resultat[i].Tid);



            }

        }

        // Endre linje 
        [TestMethod]

        public void EndreLinjer_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.EditLinje(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void EndreLinjer_Edit_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EditLinje(0);
            var LinjeResult = (LinjeAdmin)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(LinjeResult.id, 0);
        }


        [TestMethod]
        public void EndreLinjer_Edit_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EditLinje(1);
            var LinjeResult = (LinjeAdmin)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(LinjeResult.id, 1);
        }



        [TestMethod]
        public void EndreLinje_Validering_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EditLinjen"] = null;

            var linje1 = new LinjeAdmin();
            


                controller.ViewData.ModelState.AddModelError("Navn", "Linje sitt navn må inneholde minst to bokstaver eller to tall, eller en blandning");

                //act
                var actionResult = (ViewResult)controller.EditLinje(linje1, 1);
                var resultat = (LinjeAdmin)actionResult.Model;

                //assert
                Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
                Assert.AreEqual(actionResult.ViewName, "");

            
        }

        [TestMethod]
        public void EndreLinje_Post_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EditLinjen"] = null;

            var innLinje = new LinjeAdmin()
            {
                id = 0,
                Navn = "test",
                Stasjoner = new List<string>(),
                Pris = 200,
                Tid = 2

            };

            var viewStasjon = new LinjeAdmin()
            {

                id = 1,
                Navn = "L4",
                Stasjoner = new List<string>(),
                Pris = 200,
                Tid = 2

            };


            //act
            var actionResult = (ViewResult)controller.EditLinje(innLinje, 1);
            var resultat = (LinjeAdmin)actionResult.Model;

            //assert
            Assert.AreEqual(viewStasjon.id, resultat.id);
            Assert.AreEqual(viewStasjon.Navn, resultat.Navn);
            Assert.AreEqual(viewStasjon.Pris, resultat.Pris);
            Assert.AreEqual(viewStasjon.Tid, resultat.Tid);
            Assert.AreEqual(viewStasjon.Stasjoner.Count, resultat.Stasjoner.Count);
         
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void EndreLinje_Post_True()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["EditLinjen"] = null;

            var Innlinje = new LinjeAdmin()
            {

                id = 1,
                Navn = "L4",
                Stasjoner = new List<string>(),
                Pris = 200,
                Tid = 2

            };

            //act
            var actionResult = (RedirectToRouteResult)controller.EditLinje(Innlinje, 1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListLinjer");


        }



        [TestMethod]
        public void DeleteLinjer_Linjer_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteLinje(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void DeleteLinjer_Linjer_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteLinje(1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListLinjer");

        }



        [TestMethod]
        public void RegistrerLinje_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;



            //act
            var actionResult = (RedirectToRouteResult)controller.RegistrerLinje();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }

        [TestMethod]
        public void RegistrerLinje_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            LinjeRegAdmin nyregAdmin = new LinjeRegAdmin()
            {
                Navn = "test",
                Pris = 200,
                AlleStasjoner = new List<StasjonAdmin>(),
                Tid = 2,
                ValgteStasjoner = new List<int>()

             };

            //act

            //act
            var actionResult = (ViewResult)controller.RegistrerLinje();
            var resultat = (LinjeRegAdmin)actionResult.Model;

            //assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(nyregAdmin.Navn, resultat.Navn);
            Assert.AreEqual(nyregAdmin.Pris, resultat.Pris);
            Assert.AreEqual(nyregAdmin.Tid, resultat.Tid);
            Assert.AreEqual(nyregAdmin.AlleStasjoner.Count, resultat.AlleStasjoner.Count);
            Assert.AreEqual(nyregAdmin.ValgteStasjoner.Count, resultat.ValgteStasjoner.Count);

        }



        [TestMethod]
        public void Registrerlinje_Validering_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistLinje"] = null;

            var innStasjon = new LinjeRegAdmin();
          




            controller.ViewData.ModelState.AddModelError("Navn", "Linje sitt navn må inneholde minst to bokstaver eller to tall, eller en blandning");

            //act
            var actionResult = (ViewResult)controller.RegistrerLinje(innStasjon);
            var resultat = (LinjeRegAdmin)actionResult.Model;

            //assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
          

            Assert.AreEqual(actionResult.ViewName, "");

        }


        [TestMethod]
        public void RegistrerLinje_Post_False()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistLinje"] = null;

            var innLinje = new LinjeRegAdmin()
            {
              
                Navn = ""



            };

            var viewStasjon =  new LinjeRegAdmin()
            {

                Navn = "test",
                Pris = 200,
                AlleStasjoner = new List<StasjonAdmin>(),
                Tid = 2,
                ValgteStasjoner = new List<int>()


            };


            //act
            var actionResult = (ViewResult)controller.RegistrerLinje(innLinje);
            var resultat = (LinjeRegAdmin)actionResult.Model;

            //assert
           
            Assert.AreEqual(viewStasjon.Navn, resultat.Navn);
            Assert.AreEqual(viewStasjon.Pris, resultat.Pris);
            Assert.AreEqual(viewStasjon.Tid, resultat.Tid);


            Assert.AreEqual(viewStasjon.AlleStasjoner.Count, resultat.AlleStasjoner.Count);
            Assert.AreEqual(viewStasjon.ValgteStasjoner.Count, resultat.ValgteStasjoner.Count);

            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void RegistrerLinje_Post_True()
        {

            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["RegistLinje"] = null;

            var innLinje = new LinjeRegAdmin()
            {

                Navn = "test",
                Pris = 200,
                AlleStasjoner = new List<StasjonAdmin>(),
                Tid = 2,
                ValgteStasjoner = new List<int>()


            };


            //act
            var actionResult = (RedirectToRouteResult)controller.RegistrerLinje(innLinje);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListLinjer");


        }


        //KUNDER
        [TestMethod]
        public void ListKunder_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.ListKunder();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void ListKunder_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;


            var forventetResultat = new List<Kunder>();
            var kunde = new Kunder()
            {
                Id = 1,
                BetalingsMetode = "Vipps",
                Epost = "Ole-hansen@hotmail.com",
                Telefon = "47281929"

            };

            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);

            //act
            var actionResult = (ViewResult)controller.ListKunder();
            var resultat = (List<Kunder>)actionResult.Model;
            //assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (int i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Id, resultat[i].Id);
                Assert.AreEqual(forventetResultat[i].BetalingsMetode, resultat[i].BetalingsMetode);
                Assert.AreEqual(forventetResultat[i].Epost, resultat[i].Epost);
                Assert.AreEqual(forventetResultat[i].Telefon, resultat[i].Telefon);

            }

        }

        [TestMethod]
        public void EndreKunde_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.EditKunder(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void EndreKunde_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.EditKunder(1);

            //assert

            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void EndreKunde_Validering_false()
        {

            //arrange
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));

            var kunde1 = new KundeAdmin();
            

            controller.ViewData.ModelState.AddModelError("Epost", "Epost er ikke riktig");

            //act
            var resultat = (ViewResult)controller.EditKunder(kunde1);

            //assert
            Assert.IsTrue(resultat.ViewData.ModelState.Count == 1);
            Assert.AreEqual(resultat.ViewName, "");

        }


        [TestMethod]
        public void EndreKunde_Post_True()
        {

            //arrange
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));

            var kunde1 = new KundeAdmin()
            {
                id = 1,
                Epost = "Ole-hansen@hotmail.com",
                Telefon = "12345678"

            };
 

            //act
            var actionResult = (RedirectToRouteResult)controller.EditKunder(kunde1);

            

            //assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListKunder");

        }


    

         [TestMethod]
        public void DeleteKunde_Delete_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteKunde(0);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


        [TestMethod]
        public void DeleteKunde_Delete_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new VyBLL(new LogikkDalStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (RedirectToRouteResult)controller.DeleteKunde(1);

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListKunder");

        }


    
       
        
        
        //NAVBAR


        [TestMethod]
        public void Adminview_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.AdminView();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void Adminview_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.AdminView();

            //assert

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Vedlikeholdview_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = null;

            //act
            var actionResult = (RedirectToRouteResult)controller.VedlikeholdView();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }



        [TestMethod]
        public void Vedlikeholdview_Admin_True()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            //act
            var actionResult = (ViewResult)controller.VedlikeholdView();

            //assert

            Assert.AreEqual(actionResult.ViewName, "");
        }



        [TestMethod]
        public void Loggut_Admin_False()
        {
            //arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            //act
            var actionResult = (RedirectToRouteResult)controller.Loggut();

            //assert

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Logginn");

        }


    }
}
