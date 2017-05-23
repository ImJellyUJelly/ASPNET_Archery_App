using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public interface IBaanServices
    {
        Baan GetBaanById(int baanId);
        Baan GetBaanByNummer(int baanNummer);
        List<Baan> ListBanen(int VerNr);
        List<Schutter> ListSchuttersOpBaan(int baanId);
        Baan GetBaanIdFromWedstrijd(int baanId, int wedId);
        void AddBaan(Baan baan);
        void EditBaan(Baan baan);
        void RemoveBaan(Baan baan);
        void AddSchutterTobaan(Schutter schutter, int baanId);
        void RemoveSchutterFromBaan(Schutter schutter, int baanId);
    }
}
