using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;
using ArcheryApplication.Classes.Enums;
using ArcheryApplication.Classes.Database.Repositories;
using ArcheryApplication.Classes.Database.SQL;

namespace ArcheryApplication
{
    public class App
    {
        /// <summary>
        /// Dit is de facade. De GUI krijgt alleen wat in deze class gecodeerd staat.
        /// </summary>
        private WedstrijdRepository wedstrijdrepo = new WedstrijdRepository(new MysqlWedstrijdLogic());

        private List<Wedstrijd> _wedstrijden;
        public App()
        {
            _wedstrijden = GetWedstrijdenFromDB();
        }
        #region Wedstrijden 

        public List<Wedstrijd> GetWedstrijdByClubNr(int clubnr)
        {
            //TODO
            return wedstrijdrepo.ListWedstrijden();
        }

        public List<Wedstrijd> GetWedstrijdByDate(string date)
        {
            //TODO
            return wedstrijdrepo.ListWedstrijden();
        }

        public List<Wedstrijd> GetWedstrijdBySoort(string soort)
        {
            //TODO
            return wedstrijdrepo.ListWedstrijden();
        }
        /// <summary>
        /// Geeft alle wedstrijden.
        /// </summary>
        /// <returns>Een list van wedstrijden.</returns>
        private List<Wedstrijd> GetWedstrijdenFromDB()
        {
            return wedstrijdrepo.ListWedstrijden();
        }

        public List<Wedstrijd> GetWedstrijden()
        {
            _wedstrijden = GetWedstrijdenFromDB();
            return _wedstrijden;
        }

        public void AddWedstrijd(string naam, string date, string wedsoort)
        {
            Soort soort = (Soort) Enum.Parse(typeof(Soort), wedsoort);
            Wedstrijd wedstrijd = new Wedstrijd(naam, soort, date);
            wedstrijdrepo.AddWedstrijd(wedstrijd);
            wedstrijdrepo.GetWedstrijdByName(wedstrijd.Naam).LaadBanen();
        }

        public void BewerkWedstrijd(string naam, string date)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deze methode returned 1 wedstrijd, nadat er op een wedstrijd in de lijst is geklikt.
        /// </summary>
        /// <param name="wedstrijdnaam">De naam van de wedstrijd</param>
        /// <param name="soort">Het Soort wedstrijd</param>
        /// <param name="date">De datum van de wedstrijd, format: dd/MM/yyyy </param>
        /// <returns>Een wedstrijd</returns>
        public Wedstrijd GetWedstrijd(string wedstrijdnaam, string soort, string date)
        {
            throw new NotImplementedException();
        }

        public Wedstrijd GetWedstrijdById(int id)
        {
            return wedstrijdrepo.GetWedstrijdById(id);
        }

        /// <summary>
        /// De geselecteerde wedstrijd verwijderen.
        /// </summary>
        /// <param name="naam">De naam van de wedstrijd</param>
        /// <param name="date">Datum van de wedstrijd, format: dd/MM/yyyy </param>
        public void VerwijderWedstrijd(string naam, string date)
        {
            foreach (Wedstrijd w in wedstrijdrepo.ListWedstrijden())
            {
                if (w.Naam == naam)
                {
                    if (w.Datum == date)
                    {
                        wedstrijdrepo.RemoveWedstrijd(w);
                    }
                }
            }
        }
        #endregion
        #region Schutters
        /// <summary>
        /// Geeft alle schutters.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd waar deze schutter aan meedoet </param>
        /// <returns> List met schutters </returns>
        public List<Schutter> GetSchuttersList(int wedstrijdId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Geeft een schutter.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd waar deze schutter aan meedoet </param>
        /// <param name="bondsnummers"> Het bondsnummer van de persoon, als deze niet bekend is, is het -1 </param>
        /// <param name="naam"> De naam van de schutter (voor- en achternaam) </param>
        /// <returns> Een schutter </returns>
        public Schutter GetSchutter(int wedstrijdId, int bondsnummers, string naam)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Voegt een schutter toe aan een wedstrijd.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="bondsnummer"> Als schutter geen bondsnummer heeft, is het -1 </param>
        /// <param name="naam"> Naam van de schutter </param>
        /// <param name="geboortedatum"> Geboortedatum van de schutter, formaat: dd/MM/yyyy </param>
        /// <param name="geslacht"> Geslacht van de schutter: Man of Vrouw </param>
        /// <param name="discipline"> Soort boog waarmee geschoten wordt: recurve, compound, barebow of crossbow </param>
        /// <param name="klasse"> Klasse waarin geschoten wordt: aspiranten, cadetten, junioren, senioren, veteranen </param>
        /// <param name="opmerking"> Als de schutter een handicap heeft, of wat er ook te melden moet zijn voor de wedstrijd </param>
        public void SchutterAanmelden(int wedstrijdId, int bondsnummer, string naam, string geboortedatum, string geslacht, string discipline, string klasse, string opmerking)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Geef 1 score aan een schutter.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="score"> De score is de hoeveelheid punten van alle pijlen van 1 afstand </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void GeefScoreAanSchutter(int wedstrijdId, int score, int schutterId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deze methode is voor het bewerken van een schutter. Alle informatie wordt geupdated.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="bondsnummer"> Als schutter geen bondsnummer heeft, is het -1 </param>
        /// <param name="naam"> Naam van de schutter </param>
        /// <param name="geboortedatum"> Geboortedatum van de schutter, formaat: dd/MM/yyyy </param>
        /// <param name="geslacht"> Geslacht van de schutter: Man of Vrouw </param>
        /// <param name="discipline"> Soort boog waarmee geschoten wordt: recurve, compound, barebow of crossbow </param>
        /// <param name="klasse"> Klasse waarin geschoten wordt: aspiranten, cadetten, junioren, senioren, veteranen </param>
        /// <param name="opmerking"> Als de schutter een handicap heeft, of wat er ook te melden moet zijn voor de wedstrijd </param>
        public void BewerkSchutterInformatie(int wedstrijdId, int bondsnummer, string naam, string geboortedatum, string geslacht, string discipline, string klasse, string opmerking)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Banen
        /// <summary>
        /// Haalt alle banen op de bij een specifieke wedstrijd horen.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <returns> Een lijst van banen, van een specifieke wedstrijd </returns>
        public List<Baan> GetBanen(int wedstrijdId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Voegt een schutter toe aan een specifieke baan, in een specifieke wedstrijd.
        /// </summary>
        /// <param name="baanId"> Het ID van een baan </param>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void VoegSchutterToeAanBaan(int baanId, int wedstrijdId, int schutterId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verwijderd een schutter van een specifieke baan, in een specifieke wedstrijd.
        /// </summary>
        /// <param name="baanId"> Het ID van een baan </param>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void VerwijderSchutterVanBaaN(int baanId, int wedstrijdId, int schutterId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
