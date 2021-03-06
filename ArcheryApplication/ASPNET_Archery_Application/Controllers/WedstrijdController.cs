﻿using System;
using ArcheryApplication;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using ArcheryApplication.Classes;

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
            var wedstrijd = app.GetWedstrijdById(id);
            return View(wedstrijd);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                int id = Convert.ToInt32(form["Id"]);
                string naam = form["Naam"];
                string soort = form["Soort"];
                string datum = form["Datum"];

                app.BewerkWedstrijd(id, naam, soort, datum);
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
        public ActionResult AddSchutter(int id, int wedId)
        {
            try
            {
                var baan = app.GetWedstrijdBaanById(id, wedId);
                var schutter = new Schutter();
                schutter.GeefSchutterEenBaan(baan);
                return View(schutter);
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
                var baan = app.GetWedstrijdBaanById(Convert.ToInt32(schutterCollection["Baan.Id"]), Convert.ToInt32(schutterCollection["Baan.Wedstrijd.Id"]));
                string naam = schutterCollection["Naam"];
                int bondsnummer = Convert.ToInt32(schutterCollection["Bondsnummer"]);
                var schutter = app.GetSchutterByBondsnummer(bondsnummer);
                if (schutter == null)
                {
                    schutter = app.GetSchutterByName(naam);
                }
                string discipline = schutterCollection["Discipline"];
                if (schutter == null)
                {
                    string klasse = schutterCollection["Klasse"];
                    string geslacht = schutterCollection["Geslacht"];
                    string email = schutterCollection["Emailadres"];
                    string geboortedatum = schutterCollection["Geboortedatum"];
                    string opmerking = schutterCollection["Opmerking"];
                    string vereniging = schutterCollection["Vereniging.Naam"];

                    app.SchutterAanmelden(bondsnummer, naam, email, geboortedatum, geslacht, discipline, klasse, opmerking, vereniging);
                    schutter = app.GetSchutterByBondsNrEnNaam(bondsnummer, naam);
                }

                if (schutterCollection["Baan.Id"] != null)
                {
                    if (app.RegistreerSchutterOpWedstrijd(baan.Wedstrijd.Id, schutter.Id, discipline))
                    {
                        app.VoegSchutterToeAanBaan(baan.Id, baan.Wedstrijd.Id, schutter.Id);
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = baan.Wedstrijd.Id });
                    }
                }

                return RedirectToAction("Details", new { id = baan.Wedstrijd.Id });
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EditSchutter(int id, int wedId)
        {
            try
            {
                var schutter = app.GetWedstrijdSchutterById(wedId, id);
                if (schutter != null)
                {
                    return View(schutter);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult EditSchutter(FormCollection schutterCollection)
        {
            try
            {
                int wedstrijdId = Convert.ToInt32(schutterCollection["Baan.Wedstrijd.Id"]);
                int baanId = Convert.ToInt32(schutterCollection["Baan.Id"]);
                int schutterId = Convert.ToInt32(schutterCollection["Id"]);

                var schutter = app.GetWedstrijdSchutterById(wedstrijdId, schutterId);

                string naam = schutterCollection["Naam"];
                int bondsnummer = Convert.ToInt32(schutterCollection["Bondsnummer"]);
                string discipline = schutterCollection["Discipline"];
                string klasse = schutterCollection["Klasse"];
                string geslacht = schutterCollection["Geslacht"];
                string email = schutterCollection["Emailadres"];
                string geboortedatum = schutterCollection["Geboortedatum"];
                string opmerking = schutterCollection["Opmerking"];

                if (schutter != null)
                {
                    app.BewerkSchutterInformatie(wedstrijdId, baanId, schutterId,
                        bondsnummer, naam, email, geboortedatum, geslacht, discipline, klasse, opmerking);
                }
                return RedirectToAction("Details", new { id = wedstrijdId });
            }

            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult RemoveSchutter(int id, int wedid)
        {
            try
            {
                app.UnregistreerSchutterVanWedstrijd(wedid, id);
                return RedirectToAction("Details", new { id = wedid });
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var wedstrijd = app.GetWedstrijdById(id);
                foreach (var b in wedstrijd.GetBanen())
                {
                    if (b.Schutter != null)
                    {
                        app.UnregistreerSchutterVanWedstrijd(wedstrijd.Id, b.Schutter.Id);
                        app.VerwijderSchutterVanBaan(b.Id, wedstrijd.Id, b.Schutter.Id);
                    }
                }
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