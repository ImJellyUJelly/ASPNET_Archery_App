using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{ 
    public interface IWedstrijdServices
    {
        Wedstrijd GetWedstrijdByName(string naam);
        Wedstrijd GetWedstrijdById(int wedstrijdId);
        Wedstrijd GetWedstrijdByDate(string date);
        List<Wedstrijd> ListWedstrijden();
        void AddWedstrijd(Wedstrijd wedstrijd);
        void EditWedstrijd(Wedstrijd wedstrijd);
        void RemoveWedstrijd(Wedstrijd wedstrijd);
    }
}