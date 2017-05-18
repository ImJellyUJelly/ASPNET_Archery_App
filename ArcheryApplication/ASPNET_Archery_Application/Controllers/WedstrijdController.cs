using System;
using System.Collections.Generic;
using ArcheryApplication;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArcheryApplication.Classes;
using ArcheryApplication.Classes.Database.Repositories;
using ArcheryApplication.Classes.Enums;

namespace ASPNET_Archery_Application.Controllers
{
    public class WedstrijdController : Controller
    {
        private App app = new App();
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                string naam = form["Naam"];
                string soort = form["Soort"];
                string datum = form["Datum"];
                
                app.AddWedstrijd(naam, datum, soort);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Wedstrijd wedstrijd = app.GetWedstrijdById(id);
            return View(wedstrijd);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                var wedstrijd = app.GetWedstrijdById(id);
                return View(wedstrijd);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult AddSchutter(int id)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddSchutter(FormCollection schutterCollection)
        {
            try
            {
                int wedId = Convert.ToInt32(schutterCollection["WedId"]);
                string naam = schutterCollection["Naam"];
                int bondsnummer = Convert.ToInt32(schutterCollection["Bondsnummer"]);
                Schutter schutter = null;
                schutter = app.GetSchutterByBondsNrEnNaam(wedId, bondsnummer, naam);
                if (schutter == null)
                {
                    Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), schutterCollection["Klasse"]);
                    Discipline discipline = (Discipline)Enum.Parse(typeof(Discipline), schutterCollection["Discipline"]);
                    Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), schutterCollection["Geslacht"]);
                    string email = schutterCollection["Emailadres"];
                    DateTime geboortedatum = DateTime.Parse(schutterCollection["Geboortedatum"]);
                    string opmerking = schutterCollection["Opmerking"];
                    int vereniging = 1034;

                    schutter = new Schutter(bondsnummer, naam, email, klasse, discipline, geslacht, geboortedatum, opmerking);
                    app.GeefSchutterEenClub(wedId, bondsnummer, naam, vereniging);
                }

                if (schutterCollection["BaanId"] != null)
                {
                    //app.VoegSchutterToeAanBaan(schutterCollection["BaanId"]);
                }

                //app.RegistreerSchutterOpWedstrijd(wedId, );
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EditSchutter(int id, FormCollection form)
        {
            try
            {
                //Soort soort = (Soort) Enum.Parse(typeof(Soort), form["Soort"]);
                //Wedstrijd wedstrijd = new Wedstrijd(form["Naam"], soort, form["Datum"], 1034);
                //Schutter schutter = app.GetWedstrijdSchutterById(wedstrijd.Id, Convert.ToInt32(form["schutid"]), form["naam"]);

                //Schutter schutter = app.GetWedstrijdSchutterById(wedstrijd.Id, id, "Jelle Schräder");
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Wedstrijd wedstrijd = app.GetWedstrijdById(id);
                app.VerwijderWedstrijd(wedstrijd.Naam, wedstrijd.Datum);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
    }
}