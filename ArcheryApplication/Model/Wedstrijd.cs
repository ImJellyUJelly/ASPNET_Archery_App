using System.Collections.Generic;
using Model.Enums;
using Model.Exceptions;

namespace Model
{
    public class Wedstrijd
    {
        List<Baan> _banen = new List<Baan>();
        List<Schutter> _schutters = new List<Schutter>();
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public Soort Soort { get; private set; }
        public string Datum { get; private set; }

        public Wedstrijd(int id, string naam, Soort soort, string datum)
        {
            Id = id;
            Naam = naam;
            Soort = soort;
            Datum = datum;
            AantalBanenBepalen();
            //LaadSchutters();
        }
        public Wedstrijd(string naam, Soort soort, string datum)
        {
            Naam = naam;
            Soort = soort;
            Datum = datum;
            AantalBanenBepalen();
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
            // Check klopt nog niet
            if (SchutterCheck(nieuweSchutter.Bondsnummer) == false)
            {
                _schutters.Add(nieuweSchutter);
                if (nieuweSchutter.Baan == null)
                {
                    Schuttersaanbaantoevoegen();
                }
                return string.Format("Gelukt! {0} is aangemeld voor {1}", nieuweSchutter.Naam, Naam);
            }

            throw new SchutterException(string.Format("Error: {0} met bondsnummer {1} is al aangemeld voor {2}",
                nieuweSchutter.Naam, nieuweSchutter.Bondsnummer, Soort));

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
                    //Voor sommige wedstrijden kan het zijn dat een schutter nog geen bondsnummer heeft, dus wordt -1 ingevuld.
                    if (bondsnummer != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void AantalBanenBepalen()
        {
            int banen;
            if (Soort == Soort.WA1440)
            {
                banen = 18;
                BanenAanmaken(banen);
            }
            else if (Soort == Soort.JeugdFITA)
            {
                banen = 18;
                BanenAanmaken(banen);
            }
            else if (Soort == Soort.Indoorcompetitie)
            {
                banen = 10;
                BanenAanmaken(banen);
            }
            else if (Soort == Soort.Veld)
            {
                throw new SchutterException("Wij organiseren geen veldwedstrijden.");
            }
            else if (Soort == Soort.Face2Face)
            {
                banen = 19;
                BanenAanmaken(banen);
            }
            else
            {
                throw new NormalException("Er is wat fout gegaan, raadpleeg uw nerd voor meer informatie.");
            }
        }

        public List<Schutter> GetSchutters()
        {
            return _schutters;
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

        private void BanenAanmaken(int aantalBanen)
        {
            for (int baannr = 1; baannr <= aantalBanen; baannr++)
            {
                int letter = 0;
                for (int i = 0; i < 4; i++)
                {

                    if (letter == 0)
                    {
                        _banen.Add(new Baan(baannr, "A", 70));
                        letter++;
                    }
                    else if (letter == 1)
                    {
                        _banen.Add(new Baan(baannr, "B", 70));
                        letter++;
                    }
                    else if (letter == 2)
                    {
                        _banen.Add(new Baan(baannr, "C", 70));
                        letter++;
                    }
                    else if (letter == 3)
                    {
                        _banen.Add(new Baan(baannr, "D", 70));
                        letter = 0;
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{Naam} - datum: {Datum} - Soort: {Soort} - Aantal schutters: {_schutters.Count}";
        }
    }
}
