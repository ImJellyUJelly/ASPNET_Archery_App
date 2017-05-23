using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;


namespace ArcheryApplication.Storage
{ 
    public interface IBaanindelingServices
    {
        List<Baan> GetWedstrijdBanen(Wedstrijd wedstrijd);
        Baan GetBaanIdFromWedstrijd(int baanId, int wedstrijdId);
        void AddBaanToWedstrijd(Baan baan, int wedstrijdId);
        void RemoveBanenFromWedstrijd(Wedstrijd wedstrijd, int baanid);
        void AddSchutterToBaan(int wedId, int schutterId, int baanId);
        void VerwijderSchutterVanBaan(int wedId, int schutterId, int baanId);
    }
}
