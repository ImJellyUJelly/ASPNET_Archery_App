using System;
using System.Collections.Generic;

namespace ArcheryApplication.Classes.Database.Interfaces
{
    public interface IWedstrijdServices
    {
        Wedstrijd GetWedstrijdByName(string naam);
        Wedstrijd GetWedstrijdById(int wedstrijdId);
        Wedstrijd GetWedstrijdByDate(DateTime date);
        List<Wedstrijd> ListWedstrijden();
        void AddWedstrijd(Wedstrijd wedstrijd);
        void EditWedstrijd(Wedstrijd wedstrijd);
        void RemoveWedstrijd(Wedstrijd wedstrijd);
        List<Baan> GetWedstrijdBanen(Wedstrijd wedstrijd);
        List<Schutter> GetWedstrijdSchutters(Wedstrijd wedstrijd);
        Schutter GetSchutterById(int wedId, int bondsnr, string naam);
        void AddBaanToWedstrijd(Baan baan, int wedstrijdId);
        void EditBaanFromWedstrijd(Baan baan, int wedstrijdId);
        void RemoveBanenFromWedstrijd(Wedstrijd wedstrijd, int baanid);
        void RemoveSchutterFromWedstrijd(int wedId, int schutterId);
        Vereniging GetVerenigingById(int verNr);
        void AddSchutterToBaan(int wedId, int schutterId, int baanId, int afstand);
        void BewerkSchutterOpBaan(int wedId, int schutterId, int baanId);
        void VerwijderSchutterVanBaan(int wedId, int schutterId, int baanId);
        void SubscribeSchutterVoorWedstrijd(int wedId, int schutterId, string discipline);
        void UnsubscribeSchutterVoorWedstrijd(int wedId, int schutterId);
    }
}
