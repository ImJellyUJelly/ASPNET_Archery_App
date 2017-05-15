using System;
using System.Collections.Generic;
using ArcheryApplication.Classes.Database.Interfaces;

namespace ArcheryApplication.Classes.Database.Repositories
{
    public class WedstrijdRepository
    {
        private IWedstrijdServices _wedstrijdLogic;
        public WedstrijdRepository(IWedstrijdServices wedstrijdLogic)
        {
            this._wedstrijdLogic = wedstrijdLogic;
        }

        public void AddBaanToWedstrijd(Baan baan, int wedstrijdId)
        {
            _wedstrijdLogic.AddBaanToWedstrijd(baan, wedstrijdId);
        }

        public void AddWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.AddWedstrijd(wedstrijd);
        }

        public void EditBaanFromWedstrijd(Baan baan, int wedstrijdId)
        {
            _wedstrijdLogic.EditBaanFromWedstrijd(baan, wedstrijdId);
        }

        public void EditWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.EditWedstrijd(wedstrijd);
        }

        public Wedstrijd GetWedstrijdByDate(DateTime date)
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

        public void RemoveBanenFromWedstrijd(Wedstrijd wedstrijd, int baanid)
        {
            _wedstrijdLogic.RemoveBanenFromWedstrijd(wedstrijd, baanid);
        }

        public void RemoveSchutterFromWedstrijd(int wedId, int schutterId)
        {
            _wedstrijdLogic.RemoveSchutterFromWedstrijd(wedId, schutterId);
        }

        public void RemoveWedstrijd(Wedstrijd wedstrijd)
        {
            _wedstrijdLogic.RemoveWedstrijd(wedstrijd);
        }

        public List<Baan> WedstrijdBanen(Wedstrijd wedstrijd)
        {
            return _wedstrijdLogic.GetWedstrijdBanen(wedstrijd);
        }

        public List<Schutter> GetWedstrijdSchutters(Wedstrijd wedstrijd)
        {
            return _wedstrijdLogic.GetWedstrijdSchutters(wedstrijd);
        }

        public Vereniging GetVerenigingById(int verNr)
        {
            return _wedstrijdLogic.GetVerenigingById(verNr);
        }
    }
}
