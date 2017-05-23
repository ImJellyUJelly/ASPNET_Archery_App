using System;
using ArcheryApplication;
using System.Web.Mvc;
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
                var schutter = app.GetSchutterByBondsNrEnNaam(bondsnummer, naam);
                if (schutter == null)
                {
                    string klasse = schutterCollection["Klasse"];
                    string discipline = schutterCollection["Discipline"];
                    string geslacht = schutterCollection["Geslacht"];
                    string email = schutterCollection["Emailadres"];
                    string geboortedatum = schutterCollection["Geboortedatum"];
                    string opmerking = schutterCollection["Opmerking"];
                    string vereniging = schutterCollection["Vereniging.Naam"];

                    app.SchutterAanmelden(bondsnummer, naam, email, geboortedatum, geslacht, discipline, klasse, opmerking, vereniging);
                    //app.GeefSchutterEenClub(baan.Wedstrijd.Id, bondsnummer, naam);
                }
                schutter = app.GetSchutterByBondsNrEnNaam(bondsnummer, naam);
                app.RegistreerSchutterOpWedstrijd(baan.Wedstrijd.Id, schutter.Id, schutter.Discipline.ToString());
                if (schutterCollection["Baan.Id"] != null)
                {
                    app.VoegSchutterToeAanBaan(baan.Id, baan.Wedstrijd.Id, schutter.Id, baan.Afstand);
                }

                return RedirectToAction("Details", new {id = baan.Wedstrijd.Id});
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
                return View(schutter);
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