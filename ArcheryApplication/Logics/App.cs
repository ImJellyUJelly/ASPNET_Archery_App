using System;
using System.Collections.Generic;
using System.Data;
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
        private BaanRepository baanrepo = new BaanRepository(new MysqlBaanLogic());
        private SchutterRepository schutterrepo = new SchutterRepository(new MysqlSchutterLogic());
        private VerenigingRepository verenigingrepo = new VerenigingRepository(new MysqlVerenigingLogic());
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

        public Schutter GetWedstrijdSchutterById(int wedid, int bondsnr, string naam)
        {
            return wedstrijdrepo.GetSchutterById(wedid, bondsnr, naam);
        }

        public Schutter GetSchutterByBondsNrEnNaam(int bondsnr, string naam)
        {
            return schutterrepo.GetSchutterByNameAndBondsNr(bondsnr, naam);
        }

        public void AddWedstrijd(string naam, string date, string wedsoort)
        {
            Soort soort = (Soort)Enum.Parse(typeof(Soort), wedsoort);
            Wedstrijd wedstrijd = new Wedstrijd(naam, soort, date, 1034);
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
                        List<Baan> banen = w.GetBanen();
                        try
                        {
                            foreach (Schutter s in w.GetSchutters())
                            {
                                wedstrijdrepo.RemoveSchutterFromWedstrijd(w.Id, s.Id);
                            }
                            foreach (Baan b in banen)
                            {
                                wedstrijdrepo.RemoveBanenFromWedstrijd(w, b.Id);
                            }
                            if (w.GetBanenFromDB() == null)
                            {
                                wedstrijdrepo.RemoveWedstrijd(w);
                            }
                        }
                        catch (DataException dex)
                        {
                            throw new DataException("Alle wedstrijdbanen moeten verwijderd zijn voordat de wedstrijd verwijderd kan worden. Error: " + dex.Message);
                        }
                    }
                }
            }
        }

        public Vereniging GetVerenigingByNr(int nr)
        {
            return wedstrijdrepo.GetVerenigingById(nr);
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
        /// Geeft een schutter van een bepaalde wedstrijd.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd waar deze schutter aan meedoet </param>
        /// <param name="bondsnummers"> Het bondsnummer van de persoon, als deze niet bekend is, is het -1 </param>
        /// <param name="naam"> De naam van de schutter (voor- en achternaam) </param>
        /// <returns> Een schutter </returns>
        public Schutter GetSchutter(int wedstrijdId, int bondsnummer, string naam)
        {
            return wedstrijdrepo.GetSchutterById(wedstrijdId, bondsnummer, naam);
        }

        public Schutter GetSchutterById(int schutterId)
        {
            return schutterrepo.GetSchutterById(schutterId);
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
        public void SchutterAanmelden(int bondsnummer, string naam, string email, string geboortedatum, string geslacht, string discipline, string klasse, string opmerking, string vernaam)
        {
            try
            {
                DateTime gebdatum = DateTime.Parse(geboortedatum);
                Klasse _klasse = (Klasse)Enum.Parse(typeof(Klasse), klasse);
                Geslacht _geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), geslacht);
                Discipline _discipline = (Discipline)Enum.Parse(typeof(Discipline), discipline);

                Schutter schutter = new Schutter(bondsnummer, naam, email, _klasse, _discipline, _geslacht, gebdatum, opmerking);
                schutter.SetVereniging(verenigingrepo.GetVerenigingByName(vernaam));
                schutterrepo.AddSchutter(schutter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GeefSchutterEenClub(int wedid, int bondsnr, string naam)
        {
            Schutter schutter = schutterrepo.GetSchutterByNameAndBondsNr(bondsnr, naam);
            Vereniging vereniging = wedstrijdrepo.GetVerenigingById(1034);

            schutter.SetVereniging(vereniging);
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
        public void VoegSchutterToeAanBaan(int baanId, int wedstrijdId, int schutterId, int afstand)
        {
            wedstrijdrepo.AddSchutterToBaan(wedstrijdId, schutterId, baanId, afstand);
        }

        /// <summary>
        /// Verwijderd een schutter van een specifieke baan, in een specifieke wedstrijd.
        /// </summary>
        /// <param name="baanId"> Het ID van een baan </param>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void VerwijderSchutterVanBaan(int baanId, int wedstrijdId, int schutterId)
        {
            wedstrijdrepo.VerwijderSchutterVanBaan(baanId, wedstrijdId, schutterId);
        }

        public Baan GetWedstrijdBaanById(int id, int wedId)
        {
            return baanrepo.GetBaanIdFromWedstrijd(id, wedId);
        }

        public void RegistreerSchutterOpWedstrijd(int wedId, int schutterId, string discipline)
        {
            wedstrijdrepo.SubscribeSchutterVoorWedstrijd(wedId, schutterId, discipline);
        }

        public void UnregistreerSchutterVanWedstrijd(int wedId, int schutterId)
        {
            wedstrijdrepo.UnsubscribeSchutterVoorWedstrijd(wedId, schutterId);
        }

        #endregion
    }
}
