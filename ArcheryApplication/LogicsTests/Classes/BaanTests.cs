using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArcheryApplication.Classes.Tests
{
    [TestClass()]
    public class BaanTests
    {
        private Schutter schutter;
        private Vereniging vereniging;
        private Baan baan;
        private Baan testBaan;
        private Wedstrijd wedstrijd;

        [TestInitialize()]
        public void TestInitialize()
        {
            vereniging = new Vereniging(1034, "Sint Sebastiaan");
            schutter = new Schutter(0, 161378, "Jelle Schräder", "jelleschrader@gmail.com", Klasse.Senior, Discipline.Compound, Geslacht.Heren, DateTime.Parse("15 May 1995"), "", vereniging);
            wedstrijd = new Wedstrijd(0, "JF Dag 1", Soort.JeugdFITA, "13-05-2017", vereniging);
            testBaan = new Baan(0, 1, "A", 90, wedstrijd, vereniging);
        }

        [TestMethod()]
        public void BaanTest()
        {
            baan = new Baan(1, "A", 70);
            Assert.IsNotNull(baan);
        }

        [TestMethod()]
        public void BaanTest1()
        {
            baan = new Baan(0, 1, "B", 70, wedstrijd);
            Assert.IsNotNull(baan);
        }

        [TestMethod()]
        public void BaanTest2()
        {
            baan = new Baan(1, 1, "C", 70);
            Assert.IsNotNull(baan);
        }

        [TestMethod()]
        public void BaanTest3()
        {
            baan = new Baan(2, 1, "D", 70, wedstrijd, vereniging);
            Assert.IsNotNull(baan);
        }

        [TestMethod()]
        public void SetAfstandTest()
        {
            testBaan.SetAfstand(70);
            Assert.AreEqual(70, testBaan.Afstand);
        }

        [TestMethod()]
        public void VoegSchutterToeTest()
        {
            testBaan.VoegSchutterToe(schutter);
            Assert.IsNotNull(testBaan.Schutter);
        }

        [TestMethod()]
        public void VerwijderSchutterTest()
        {
            testBaan.VoegSchutterToe(schutter);
            testBaan.VerwijderSchutter();
            Assert.IsNull(testBaan.Schutter);
        }
    }
}