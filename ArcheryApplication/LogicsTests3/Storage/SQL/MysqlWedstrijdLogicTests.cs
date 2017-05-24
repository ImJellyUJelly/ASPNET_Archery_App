using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage.Tests
{
    [TestClass()]
    public class MysqlWedstrijdLogicTests : IWedstrijdServices
    {
        private List<Wedstrijd> wedstrijden;
        private Wedstrijd testWedstrijd;
        private Wedstrijd wedstrijd;
        private Vereniging vereniging;
        [TestInitialize()]
        public void TestInitialize()
        {
            vereniging = new Vereniging(1034, "Sint Sebstiaan");
            wedstrijden = new List<Wedstrijd>();
            wedstrijden.Add(testWedstrijd = new Wedstrijd(0, "Wedstrijd1", Soort.JeugdFITA, "01-01-2017", vereniging));
            wedstrijden.Add(testWedstrijd = new Wedstrijd(1, "Wedstrijd2", Soort.WA1440, "02-01-2017", vereniging));
            wedstrijden.Add(testWedstrijd = new Wedstrijd(2, "Wedstrijd3", Soort.Indoorcompetitie, "03-01-2017", vereniging));
            wedstrijden.Add(testWedstrijd = new Wedstrijd(3, "Wedstrijd4", Soort.Veld, "04-01-2017", vereniging));
        }

        [TestMethod()]
        public Wedstrijd GetWedstrijdByName(string naam)
        {
            foreach (Wedstrijd w in wedstrijden)
            {
                if (w.Naam == naam)
                {
                    wedstrijd = w;
                    return w;
                }
            }
            return null;
        }

        [TestMethod()]
        public Wedstrijd GetWedstrijdById(int wedstrijdId)
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public Wedstrijd GetWedstrijdByDate(string date)
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public List<Wedstrijd> ListWedstrijden()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddWedstrijd(Wedstrijd wedstrijd)
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void EditWedstrijd(Wedstrijd wedstrijd)
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RemoveWedstrijd(Wedstrijd wedstrijd)
        {
            throw new NotImplementedException();
        }
    }
}