using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public interface ISchutterServices
    {
        Schutter GetSchutterById(int schutterId);
        Schutter GetSchutterByBondsNr(int bondsnr);
        Schutter GetSchutterByNameAndBondsNr(int bondsnr, string naam);
        List<Schutter> ListSchutters();
        void AddSchutter(Schutter schutter);
        void EditSchutter(Schutter schutter);
        void RemoveSchutter(Schutter schutter);
    }
}
