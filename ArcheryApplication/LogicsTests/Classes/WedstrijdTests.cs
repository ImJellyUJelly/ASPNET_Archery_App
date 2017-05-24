using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ArcheryApplication.Classes.Tests
{
    [TestClass()]
    public class WedstrijdTests
    {
        private Schutter schutter;
        private Wedstrijd testWedstrijd;
        private Vereniging vereniging;
        private List<Baan> banen;
        [TestInitialize()]
        public void TestInitialize()
        {
            vereniging = new Vereniging(1034, "Sint Sebastiaan");
            testWedstrijd = new Wedstrijd(0, "JF Dag 2", Soort.JeugdFITA, "14-05-2017", vereniging);
            schutter = new Schutter(0, 161378, "Jelle Schräder", "jelleschrader@gmail.com", Klasse.Senior, Discipline.Compound, Geslacht.Heren, DateTime.Parse("15 May 1995"), "", vereniging);
            testWedstrijd.SchutterAanmelden(schutter);
            banen = new List<Baan>();
            banen.Add(new Baan(0, 1, "A", 70, testWedstrijd, vereniging));
            banen.Add(new Baan(1, 1, "B", 70, testWedstrijd, vereniging));
            banen.Add(new Baan(2, 1, "C", 70, testWedstrijd, vereniging));
            banen.Add(new Baan(3, 1, "D", 70, testWedstrijd, vereniging));
        }

        [TestMethod()]
        public void BewerkWedstrijdTest()
        {
            Wedstrijd andereWedstrijd = new Wedstrijd(testWedstrijd.Id, testWedstrijd.Naam, testWedstrijd.Soort, testWedstrijd.Datum, testWedstrijd.Vereniging);
            testWedstrijd.BewerkWedstrijd("JF Dag 1", Soort.JeugdFITA, "13-05-2017");
            Assert.AreNotEqual(andereWedstrijd, testWedstrijd);
        }

        [TestMethod()]
        public void DeleteBaanTest()
        {
            testWedstrijd.DeleteBaan(3);
            Assert.AreNotEqual(4, testWedstrijd.GetBanen().Count);
        }

        [TestMethod()]
        public void GetSchuttersTest()
        {
            List<Schutter> schutters = testWedstrijd.GetSchutters();
            Assert.IsNotNull(schutters);
        }
    }
}