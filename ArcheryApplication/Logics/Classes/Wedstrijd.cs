﻿using System;
using System.Collections.Generic;
using ArcheryApplication.Storage;

namespace ArcheryApplication.Classes
{ 
    public class Wedstrijd 
    {
        private VerenigingRepository verenigingrepo = new VerenigingRepository(new MysqlVerenigingLogic());
        private RegistratieRepository registratierepo = new RegistratieRepository(new MysqlRegisterLogic());
        private BaanindelingRepository baanindelingrepo = new BaanindelingRepository(new MysqlBaanindelingLogic());
        List<Baan> _banen = new List<Baan>();
        List<Schutter> _schutters = new List<Schutter>();
        public int Id { get; private set; }
        public string Naam { get; set; }
        public Soort Soort { get; set; }
        public string Datum { get; set; }
        public Vereniging Vereniging { get; private set; }
        public Wedstrijd(int id, string naam, Soort soort, string datum, Vereniging vereniging)
        {
            Id = id;
            Naam = naam;
            Soort = soort;
            Datum = datum;
            Vereniging = vereniging;
            LaadBanen();
        }
        public Wedstrijd(string naam, Soort soort, string datum, int vereniging)
        {
            Naam = naam;
            Soort = soort;
            Datum = datum;
            Vereniging = verenigingrepo.GetVerenigingById(vereniging);
            LaadBanen();
        }

        //edit stuff
        public void BewerkWedstrijd(string naam, Soort soort, string datum)
        {
            Naam = naam;
            Soort = soort;
            Datum = datum;
        }


        public string SchutterAanmelden(Schutter nieuweSchutter)
        {
            if (SchutterCheck(nieuweSchutter.Bondsnummer))
            {
                _schutters.Add(nieuweSchutter);
                if (nieuweSchutter.Baan == null)
                {
                    Schuttersaanbaantoevoegen();
                }
                return string.Format("Gelukt! {0} is aangemeld voor {1}", nieuweSchutter.Naam, Naam);
            }
            else
            {
                return string.Format("Error: {0} met bondsnummer {1} is al aangemeld voor {2}",
                    nieuweSchutter.Naam, nieuweSchutter.Bondsnummer, Soort);
            }
        }
        public void SchutterAanmeldenOpBaan(Schutter nieuweSchutter)
        {
            SchutterAanmelden(nieuweSchutter);
            foreach (Baan b in _banen)
            {
                if (b.BaanId == nieuweSchutter.Baan.BaanId)
                {
                    b.VoegSchutterToe(nieuweSchutter);
                }
            }
        }

        private bool SchutterCheck(int bondsnummer)
        {
            foreach (Schutter s in _schutters)
            {
                if (bondsnummer == s.Bondsnummer)
                {
                    if (bondsnummer != -1)
                    {
                        throw new SchutterException("Schutter is al aangemeld voor deze wedstrijd.");
                    }
                }
            }
            return true;
        }

        public void DeleteBaan(int baanId)
        {
            foreach (Baan b in _banen)
            {
                if (b.Id == baanId)
                {
                    _banen.Remove(b);
                    break;
                }
            }
        }

        public void LaadBanen()
        {
            try
            {
                if (Id != 0)
                {
                    List<Baan> banenUitDb = baanindelingrepo.GetWedstrijdBanen(this);
                    if (banenUitDb != null)
                    {
                        _banen = banenUitDb;
                    }
                    else
                    {
                        AantalBanenBepalen();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AantalBanenBepalen()
        {
            int banen;
            if (Soort == Soort.WA1440)
            {
                banen = 18;
                BaanIndelingMaken(banen);
            }
            else if (Soort == Soort.JeugdFITA)
            {
                banen = 18;
                BaanIndelingMaken(banen);
            }
            else if (Soort == Soort.Indoorcompetitie)
            {
                banen = 10;
                BaanIndelingMaken(banen);
            }
            else if (Soort == Soort.Veld)
            {
                throw new WedstrijdException(@"Kies een andere soort, wij organiseren geen veld.");
            }
            else if (Soort == Soort.Face2Face)
            {
                banen = 19;
                BaanIndelingMaken(banen);
            }
            else

            {
                throw new WedstrijdException(@"Volgens mij gaat er wat mis, aantal banen is niet bepaald.");
            }
        }

        public List<Schutter> GetSchutters()
        {
            return _schutters;
        }

        public List<Baan> GetBanenFromDb()
        {
            return baanindelingrepo.GetWedstrijdBanen(this);
        }

        public List<Baan> GetBanen()
        {
            return _banen;
        }
        // Als de schutter geen baan heeft geselecteerd
        public void Schuttersaanbaantoevoegen()
        {
            if (_schutters != null)
            {
                foreach (Schutter s in _schutters)
                {
                    if (s.Baan == null)
                    {
                        foreach (Baan b in _banen)
                        {
                            if (b.Schutter == null)
                            {
                                b.VoegSchutterToe(s);
                            }
                            if (s.Baan != null)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void BaanIndelingMaken(int aantalBanen)
        {
            aantalBanen *= 4;
            List<Baan> banen = verenigingrepo.GetListBanen(Vereniging.VerNr);
            for (int baannr = 0; baannr < aantalBanen; baannr++)
            {
                CreateBaan(banen, baannr);
            }
        }

        private void CreateBaan(List<Baan> banen, int baanid)
        {
            Baan baan;
            baan = banen[baanid];
            baan.SetAfstand(70);
            baanindelingrepo.AddBaanToWedstrijd(baan, Id);
            _banen.Add(baan);
        }

        public override string ToString()
        {
            return $"{Naam} - datum: {Datum} - Soort: {Soort} - Aantal schutters: {_schutters.Count}";
        }
    }
}
