using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public interface IBaanServices
    {
        Baan GetBaanById(int baanId);
        Baan GetBaanByNummer(int baanNummer);
        void AddBaan(Baan baan);
        void EditBaan(Baan baan);
        void RemoveBaan(Baan baan);
    }
}
