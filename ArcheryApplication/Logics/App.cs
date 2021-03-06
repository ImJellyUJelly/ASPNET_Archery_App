﻿using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using ArcheryApplication.Storage;

namespace ArcheryApplication
{
    public class App
    {
        private WedstrijdRepository wedstrijdrepo = new WedstrijdRepository(new MysqlWedstrijdLogic());
        private BaanRepository baanrepo = new BaanRepository(new MysqlBaanLogic());
        private SchutterRepository schutterrepo = new SchutterRepository(new MysqlSchutterLogic());
        private VerenigingRepository verenigingrepo = new VerenigingRepository(new MysqlVerenigingLogic());
        private RegistratieRepository registratierepo = new RegistratieRepository(new MysqlRegisterLogic());
        private BaanindelingRepository baanindelingrepo = new BaanindelingRepository(new MysqlBaanindelingLogic());
        private List<Wedstrijd> _wedstrijden;

        /// <summary>
        /// Dit is de facade. De GUI krijgt alleen wat in deze class gecodeerd staat.
        /// </summary>
        public App()
        {
            try
            {
                _wedstrijden = GetWedstrijdenFromDB();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #region Wedstrijden 
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

        public Schutter GetWedstrijdSchutterById(int wedId, int schutterId)
        {
            int registratienr = registratierepo.GetRegistratieId(schutterId, wedId);
            Schutter schutter = baanindelingrepo.GetSchutterFromWedstrijd(registratienr, wedId);
            return schutter;
        }

        public Schutter GetSchutterByBondsNrEnNaam(int bondsnr, string naam)
        {
            return schutterrepo.GetSchutterByNameAndBondsNr(bondsnr, naam);
        }

        public Schutter GetSchutterByBondsnummer(int bondsnr)
        {
            return schutterrepo.GetSchutterByBondsNr(bondsnr);
        }

        public void AddWedstrijd(string naam, string date, string wedsoort)
        {
            Soort soort = (Soort)Enum.Parse(typeof(Soort), wedsoort);
            Wedstrijd wedstrijd = new Wedstrijd(naam, soort, date, 1034);
            wedstrijdrepo.AddWedstrijd(wedstrijd);
            wedstrijdrepo.GetWedstrijdByName(wedstrijd.Naam).LaadBanen();
        }

        public void BewerkWedstrijd(int id, string naam, string _soort, string date)
        {
            Wedstrijd wedstrijd = wedstrijdrepo.GetWedstrijdById(id);
            Soort soort = (Soort)Enum.Parse(typeof(Soort), _soort);
            wedstrijd.BewerkWedstrijd(naam, soort, date);
            wedstrijdrepo.EditWedstrijd(wedstrijd);
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
                                registratierepo.UnsubscribeSchutterVoorWedstrijd(w.Id, s.Id);
                            }
                            foreach (Baan b in banen)
                            {
                                baanindelingrepo.RemoveBanenFromWedstrijd(w, b.Id);
                            }
                            if (w.GetBanenFromDb() == null)
                            {
                                wedstrijdrepo.RemoveWedstrijd(w);
                            }
                        }
                        catch (DataException dex)
                        {
                            throw new DataException(
                                "Alle wedstrijdbanen moeten verwijderd zijn voordat de wedstrijd verwijderd kan worden. Error: " +
                                dex.Message);
                        }
                    }
                }
            }
        }

        public Vereniging GetVerenigingByNr(int nr)
        {
            return verenigingrepo.GetVerenigingById(nr);
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
            return schutterrepo.ListSchutters();
        }

        /// <summary>
        /// Geeft een schutter van een bepaalde wedstrijd.
        /// </summary>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd waar deze schutter aan meedoet </param>
        /// <param name="schutId"> Het ID van de persoon</param>
        /// <returns> Een schutter </returns>
        public Schutter GetSchutter(int wedstrijdId, int schutId)
        {
            return registratierepo.GetWedstrijdSchutterById(wedstrijdId, schutId);
        }

        public Schutter GetSchutterById(int schutterId)
        {
            return schutterrepo.GetSchutterById(schutterId);
        }

        public Schutter GetSchutterByName(string naam)
        {
            return schutterrepo.GetSchutterByName(naam);
        }

        /// <summary>
        /// Voegt een schutter toe aan de DB.
        /// </summary>
        /// <param name="bondsnummer"> Als schutter geen bondsnummer heeft, is het -1 </param>
        /// <param name="naam"> Naam van de schutter </param>
        /// <param name="geboortedatum"> Geboortedatum van de schutter, formaat: dd/MM/yyyy </param>
        /// <param name="_geslacht"> Geslacht van de schutter: Man of Vrouw </param>
        /// <param name="_discipline"> Soort boog waarmee geschoten wordt: recurve, compound, barebow of crossbow </param>
        /// <param name="_klasse"> Klasse waarin geschoten wordt: aspiranten, cadetten, junioren, senioren, veteranen </param>
        /// <param name="opmerking"> Als de schutter een handicap heeft, of wat er ook te melden moet zijn voor de wedstrijd </param>
        public void SchutterAanmelden(int bondsnummer, string naam, string email, string geboortedatum, string _geslacht, string _discipline, string _klasse, string opmerking, string vernaam)
        {
            try
            {
                DateTime gebdatum = DateTime.Parse(geboortedatum);
                Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), _klasse);
                Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), _geslacht);
                Discipline discipline = (Discipline)Enum.Parse(typeof(Discipline), _discipline);

                Schutter schutter = new Schutter(bondsnummer, naam, email, klasse, discipline, geslacht, gebdatum, opmerking);
                schutter.SetVereniging(verenigingrepo.GetVerenigingByName(vernaam));
                schutterrepo.AddSchutter(schutter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GeefSchutterEenClub(int bondsnr, string schutnaam)
        {
            Schutter schutter = schutterrepo.GetSchutterByNameAndBondsNr(bondsnr, schutnaam);
            Vereniging vereniging = verenigingrepo.GetVerenigingById(1034);

            schutter.SetVereniging(vereniging);
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
        public void BewerkSchutterInformatie(int wedstrijdId, int baanId, int schutterId, int bondsnummer, string naam, string email, string geboortedatum, string _geslacht, string _discipline, string _klasse, string opmerking)
        {
            try
            {
                DateTime gebdatum = DateTime.Parse(geboortedatum);
                Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), _klasse);
                Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), _geslacht);
                Discipline discipline = (Discipline)Enum.Parse(typeof(Discipline), _discipline);
                Schutter editedSchutter = new Schutter(schutterId, bondsnummer, naam, email, klasse, discipline, geslacht, gebdatum,
                    opmerking);
                editedSchutter.SetVereniging(verenigingrepo.GetVerenigingById(1034));
                schutterrepo.EditSchutter(editedSchutter);
                registratierepo.SetDisciplineFromSchutter(discipline.ToString(), schutterId, wedstrijdId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
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
            try
            {
                Wedstrijd wedstrijd = wedstrijdrepo.GetWedstrijdById(wedstrijdId);
                return baanindelingrepo.GetWedstrijdBanen(wedstrijd);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Voegt een schutter toe aan een specifieke baan, in een specifieke wedstrijd.
        /// </summary>
        /// <param name="baanId"> Het ID van een baan </param>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void VoegSchutterToeAanBaan(int baanId, int wedstrijdId, int schutterId)
        {
            int registratieId = registratierepo.GetRegistratieId(schutterId, wedstrijdId);
            baanindelingrepo.AddSchutterToBaan(wedstrijdId, registratieId, baanId);
        }

        /// <summary>
        /// Verwijderd een schutter van een specifieke baan, in een specifieke wedstrijd.
        /// </summary>
        /// <param name="baanId"> Het ID van een baan </param>
        /// <param name="wedstrijdId"> Het ID van een wedstrijd </param>
        /// <param name="schutterId"> Het ID van een schutter </param>
        public void VerwijderSchutterVanBaan(int baanId, int wedstrijdId, int schutterId)
        {
            baanindelingrepo.VerwijderSchutterVanBaan(baanId, wedstrijdId, schutterId);
        }

        public Baan GetWedstrijdBaanById(int id, int wedId)
        {
            return baanindelingrepo.GetBaanIdFromWedstrijd(id, wedId);
        }

        public bool RegistreerSchutterOpWedstrijd(int wedId, int schutterId, string discipline)
        {
            Schutter result;
            result = baanindelingrepo.GetSchutterFromWedstrijd(schutterId, wedId);
            Discipline _discipline = (Discipline)Enum.Parse(typeof(Discipline), discipline);
            if (result == null)
            {
                registratierepo.SubscribeSchutterVoorWedstrijd(wedId, schutterId, discipline);
                return true;
            }
            return false;
        }

        public void UnregistreerSchutterVanWedstrijd(int wedId, int schutterId)
        {
            registratierepo.UnsubscribeSchutterVoorWedstrijd(wedId, schutterId);
        }

        #endregion
    }
}
