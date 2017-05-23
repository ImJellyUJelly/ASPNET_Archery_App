using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{ 
    public class WedstrijdRepository
    {
        private IWedstrijdServices _wedstrijdLogic;

        public WedstrijdRepository(IWedstrijdServices wedstrijdLogic)
        {
            _wedstrijdLogic = wedstrijdLogic;
        }

        public void AddWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.AddWedstrijd(wedstrijd);
        }

        public void EditWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.EditWedstrijd(wedstrijd);
        }

        public Wedstrijd GetWedstrijdByDate(string date)
        {
            return _wedstrijdLogic.GetWedstrijdByDate(date);
        }

        public Wedstrijd GetWedstrijdByName(string naam)
        {
            return _wedstrijdLogic.GetWedstrijdByName(naam);
        }

        public Wedstrijd GetWedstrijdById(int wedstrijdId)
        {
            return _wedstrijdLogic.GetWedstrijdById(wedstrijdId);
        }

        public List<Wedstrijd> ListWedstrijden()
        {
            return _wedstrijdLogic.ListWedstrijden();
        }

        public void RemoveWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.RemoveWedstrijd(wedstrijd);
        }
    }
}
