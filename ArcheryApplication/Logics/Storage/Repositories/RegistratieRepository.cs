using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public class RegistratieRepository
    {
        private IRegistratieServices _registratieLogic;

        public RegistratieRepository(IRegistratieServices registratieLogic)
        {
            _registratieLogic = registratieLogic;
        }

        public List<Schutter> GetWedstrijdSchutters(Wedstrijd wedstrijd)
        {
            return _registratieLogic.GetWedstrijdSchutters(wedstrijd);
        }

        public Schutter GetWedstrijdSchutterById(int wedId, int schutterId)
        {
            return _registratieLogic.GetWedstrijdSchutterById(wedId, schutterId);
        }

        public int GetRegistratieId(int schutId, int wedId)
        {
            return _registratieLogic.GetRegistratieId(schutId, wedId);
        }

        public void SubscribeSchutterVoorWedstrijd(int wedId, int schutterId, string discipline)
        {
            _registratieLogic.SubscribeSchutterVoorWedstrijd(wedId, schutterId, discipline);
        }

        public void UnsubscribeSchutterVoorWedstrijd(int wedId, int schutterId)
        {
            _registratieLogic.UnsubscribeSchutterVoorWedstrijd(wedId, schutterId);
        }

        public void SetDisciplineFromSchutter(string discipline, int schutterId, int wedId)
        {
            _registratieLogic.SetDisciplineFromSchutter(discipline, schutterId, wedId);
        }
    }
}
