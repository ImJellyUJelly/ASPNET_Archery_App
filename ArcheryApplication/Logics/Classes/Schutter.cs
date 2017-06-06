using System;

namespace ArcheryApplication.Classes
{
    public class Schutter
    {
        public int Id { get; private set; }
        public int Bondsnummer { get;  set; }
        public string Naam { get;  set; }
        public DateTime Geboortedatum { get;  set; }
        public string Emailadres { get;  set; }
        public Baan Baan { get; private set; }
        public Klasse Klasse { get;  set; }
        public Discipline Discipline { get;  set; }
        public Geslacht Geslacht { get;  set; }
        public string Opmerking { get;  set; }
        public Scoreformulier ScoreFormulier { get; private set; }
        public Vereniging Vereniging { get;  set; }

        // Algemene constructor
        public Schutter(int bondsnr, string naam, string email, Klasse k, Discipline d, Geslacht g, DateTime geb, string opmerking)
        {
            Bondsnummer = bondsnr;
            Naam = naam;
            Emailadres = email;
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

        public Schutter(int id, int bondsnummer, string schutnaam, string email, Klasse klasse, Geslacht geslacht, DateTime gebdatum, string opmerking, Vereniging vereniging)
        {
            Id = id;
            Bondsnummer = bondsnummer;
            Naam = schutnaam;
            Emailadres = email;
            Klasse = klasse;
            Geslacht = geslacht;
            Geboortedatum = gebdatum;
            Opmerking = opmerking;
            Vereniging = vereniging;
            ScoreFormulier = new Scoreformulier();
        }

        public Schutter()
        {
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

        public void SetVereniging(Vereniging vereniging)
        {
            Vereniging = vereniging;
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
