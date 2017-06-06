using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public interface IRegistratieServices
    {
        List<Schutter> GetWedstrijdSchutters(Wedstrijd wedstrijd);
        Schutter GetWedstrijdSchutterById(int wedId, int schutterId);
        int GetRegistratieId(int schutterId, int wedstrijdId);
        void SetDisciplineFromSchutter(string discipline, int schutterId, int wedId);
        void SubscribeSchutterVoorWedstrijd(int wedId, int schutterId, string discipline);
        void UnsubscribeSchutterVoorWedstrijd(int wedId, int schutterId);
    }
}
