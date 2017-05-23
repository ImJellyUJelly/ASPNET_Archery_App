using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArcheryApplication.Classes.Tests
{
    [TestClass()]
    public class SchutterTests
    {
        private Baan baan;
        private Vereniging vereniging;
        private Wedstrijd wedstrijd;
        private Schutter schutter;
        private Schutter testSchutter;
        private Score score;

        [TestInitialize()]
        public void TestInitialize()
        {
            vereniging = new Vereniging(1034, "Sint Sebastiaan");
            wedstrijd = new Wedstrijd(0, "JF Dag 1", Soort.JeugdFITA, "13-05-2017", vereniging);
            baan = new Baan(0, 1, "A", 90, wedstrijd, vereniging);
            testSchutter = new Schutter(0, 161378, "Jelle Schräder", "jelleschrader@gmail.com", Klasse.Senior, Discipline.Compound, 
                Geslacht.Heren, DateTime.Parse("15 May 1995"), "1100 Ster", vereniging);
            score = new Score(0, 360, 90);
        }

        [TestMethod()]
        public void SchutterTest()
        {
            schutter = new Schutter(161379, "Jan de Man", "jandeman@hotmail.nl", Klasse.Senior, Discipline.Recurve,
                Geslacht.Heren, DateTime.Parse("1 January 1950"), "1350 Ster");
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void SchutterTest1()
        {
            schutter = new Schutter(2, 161380, "Henk Notenboom", "henk@live.nl", Klasse.Junior, Discipline.Longbow, 
                Geslacht.Heren, DateTime.Parse("10 December 1999"), "");
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void SchutterTest2()
        {
            schutter = new Schutter(161381, "Henk Appel", "henkappel@tester.nl", Klasse.Veteraan, Discipline.Crossbow,
                Geslacht.Heren, DateTime.Parse("4 May 1945"), "1440 Ster", baan);
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void SchutterTest3()
        {
            schutter = new Schutter(3, 161382, "Jaap Jan Hanzel", "Hanzel@gretel.du", Klasse.Aspirant, Discipline.Recurve,
                Geslacht.Heren, DateTime.Parse("14 February 2007"), "Eigenlijk te jong om te schieten.", vereniging);
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void SchutterTest4()
        {
            schutter = new Schutter(4, 161383, "Lisa Ann", "lisaann@pornhub.com", Klasse.Veteraan, Geslacht.Dames, 
                DateTime.Parse("9 May 1972"), "Schiet altijd met dikke pijlen.", vereniging);
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void SchutterTest5()
        {
            schutter = new Schutter();
            Assert.IsNotNull(schutter);
        }

        [TestMethod()]
        public void EditSchutterTest()
        {
            schutter = new Schutter(testSchutter.Id, testSchutter.Bondsnummer, testSchutter.Naam, testSchutter.Emailadres, 
                testSchutter.Klasse, testSchutter.Discipline, testSchutter.Geslacht, testSchutter.Geboortedatum, 
                testSchutter.Opmerking, testSchutter.Vereniging);
            schutter.EditSchutter(123456, "TestSchutter1", Klasse.Senior, Discipline.Compound, Geslacht.Heren,
                DateTime.Parse("15 May 1995"), "");
            Assert.AreNotEqual(testSchutter, schutter);
        }

        [TestMethod()]
        public void AddScoreTest()
        {
            testSchutter.AddScore(score);
            Assert.AreNotEqual(0, testSchutter.ScoreFormulier.GetScores());
        }

        [TestMethod()]
        public void GeefSchutterEenBaanTest()
        {
            testSchutter.GeefSchutterEenBaan(baan);
            Assert.IsNotNull(testSchutter.Baan);
        }

        [TestMethod()]
        public void SetVerenigingTest()
        {
            Vereniging nieuweVereniging = new Vereniging(1, "Somewhere, over the rainbow.");
            testSchutter.SetVereniging(nieuweVereniging);
            Assert.AreNotEqual(vereniging, nieuweVereniging);
        }

        [TestMethod()]
        public void CompareSchuttersTest()
        {
            Schutter andereSchutter = new Schutter();
            int result = testSchutter.CompareSchutters(andereSchutter);
            Assert.AreEqual(result, 1);
        }
    }
}