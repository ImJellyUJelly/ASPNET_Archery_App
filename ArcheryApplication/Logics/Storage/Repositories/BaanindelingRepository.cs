using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public class BaanindelingRepository
    {
        private IBaanindelingServices _baanindelingLogic;

        public BaanindelingRepository(IBaanindelingServices baanindelingLogic)
        {
            _baanindelingLogic = baanindelingLogic;
        }

        public List<Baan> GetWedstrijdBanen(Wedstrijd wedstrijd)
        {
            return _baanindelingLogic.GetWedstrijdBanen(wedstrijd);
        }

        public Baan GetBaanIdFromWedstrijd(int baanId, int wedId)
        {
            return _baanindelingLogic.GetBaanIdFromWedstrijd(baanId, wedId);
        }

        public void AddBaanToWedstrijd(Baan baan, int wedstrijdId)
        {
            _baanindelingLogic.AddBaanToWedstrijd(baan, wedstrijdId);
        }

        public void RemoveBanenFromWedstrijd(Wedstrijd wedstrijd, int baanid)
        {
            _baanindelingLogic.RemoveBanenFromWedstrijd(wedstrijd, baanid);
        }

        public void AddSchutterToBaan(int wedId, int schutterId, int baanId)
        {
            _baanindelingLogic.AddSchutterToBaan(wedId, schutterId, baanId);
        }

        public void VerwijderSchutterVanBaan(int wedId, int schutterId, int baanId)
        {
            _baanindelingLogic.VerwijderSchutterVanBaan(wedId, schutterId, baanId);
        }

        public Schutter GetSchutterFromWedstrijd(int schutId, int wedId)
        {
            return _baanindelingLogic.GetSchutterFromWedstrijd(schutId, wedId);
        }
    }
}
