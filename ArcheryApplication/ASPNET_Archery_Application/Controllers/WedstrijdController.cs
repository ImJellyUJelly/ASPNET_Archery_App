using System;
using System.Collections.Generic;
using ArcheryApplication;
using System.Linq;
using System.Web;
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
        public ActionResult Edit()
        {
            return View();
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

        public ActionResult EditSchutter(int id)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            Wedstrijd wedstrijd = app.GetWedstrijdById(id);
            app.VerwijderWedstrijd(wedstrijd.Naam, wedstrijd.Datum);

            return RedirectToAction("Index");
        }
    }
}