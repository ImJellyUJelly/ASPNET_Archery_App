using System;

namespace ArcheryApplication.Classes
{
    public class Schutter
    {
        public int Id { get; private set; }
        public int Bondsnummer { get; private set; }
        public string Naam { get; private set; }
        public DateTime Geboortedatum { get; private set; }
        public string EmailAdres { get; private set; }
        public Baan Baan { get; private set; }
        public Klasse Klasse { get; private set; }
        public Discipline Discipline { get; private set; }
        public Geslacht Geslacht { get; private set; }
        public string Opmerking { get; private set; }
        public Scoreformulier ScoreFormulier { get; private set; }
        public Vereniging Vereniging { get; private set; }

        // Algemene constructor
        public Schutter(int bondsnr, string naam, string email, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking)
        {
            Bondsnummer = bondsnr;
            Naam = naam;
            EmailAdres = email;
            Klasse = k;
            Discipline = d;
            Geslacht = g;
            Geboortedatum = geb;
            Opmerking = opmerking;
            ScoreFormulier = new Scoreformulier();
        }
        // Algemene constructor met ID (voor Database)
        public Schutter(int id, int bondsnr, string naam, string email, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking) : this(bondsnr, naam, email, k, d, g, geb, opmerking)
        {
            Id = id;
        }
        // Als een schutter op een bepaalde baan wil staan
        public Schutter(int bondsnr, string naam, string email, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking, Baan baan) : this(bondsnr, naam, email, k, d, g, geb, opmerking)
        {
            Baan = baan;
        }
        // Constructor met een Verenging
        public Schutter(int id, int bondsnr, string naam, string email, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking, Vereniging vereniging) : this(id, bondsnr, naam, email, k, d, g, geb, opmerking)
        {
            Vereniging = vereniging;
        }

        public void EditSchutter(int bondsnr, string naam, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking)
        {
            Bondsnummer = bondsnr;
            Naam = naam;
            Klasse = k;
            Discipline = d;
            Geslacht = g;
            Geboortedatum = geb;
            Opmerking = opmerking;
            ScoreFormulier = new Scoreformulier();
        }

        public void AddScore(Score score)
        {
            ScoreFormulier.AddScore(score);
        }
        public void GeefSchutterEenBaan(Baan b)
        {
            Baan = b;
        }

        public int CompareSchutters(Schutter andereSchutter)
        {
            int result = Bondsnummer.CompareTo(andereSchutter.Bondsnummer);
            return result;
        }

        public override string ToString()
        {
            return $"{Bondsnummer} { Naam } - Klasse: { Klasse } - Geslacht: { Geslacht } - Discipline: { Discipline } - Geb.Datum: { Geboortedatum.ToShortDateString() } - Score: { ScoreFormulier.TotaalScore.ToString() }";
        }
    }
}
