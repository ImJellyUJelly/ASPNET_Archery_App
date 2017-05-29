using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
{
    public class MysqlWedstrijdLogic : IWedstrijdServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        private MysqlVerenigingLogic _verenigingLogic;

        public void AddWedstrijd(Wedstrijd wedstrijd)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {

                            cmd.CommandText = "INSERT INTO Wedstrijd (WedNaam, WedSoort, WedDatum, WedVerNr) VALUES (@wednaam, @wedsoort, @weddatum, @wedvernr)";

                            cmd.Parameters.AddWithValue("@wednaam", wedstrijd.Naam);
                            cmd.Parameters.AddWithValue("@wedsoort", wedstrijd.Soort);
                            cmd.Parameters.AddWithValue("@weddatum", wedstrijd.Datum);
                            if (wedstrijd.Vereniging != null)
                            {
                                cmd.Parameters.AddWithValue("@wedvernr", wedstrijd.Vereniging.VerNr);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@wedvernr", 1034);
                            }

                            cmd.Connection = conn;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public void EditWedstrijd(Wedstrijd wedstrijd)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {

                            cmd.CommandText = "UPDATE Wedstrijd SET WedNaam = @wednaam AND WedSoort = @wedsoort AND WedDatum = @weddatum WHERE WedID = @wedid;";

                            cmd.Parameters.AddWithValue("@wedid", wedstrijd.Id);
                            cmd.Parameters.AddWithValue("@wednaam", wedstrijd.Naam);
                            cmd.Parameters.AddWithValue("@wedsoort", wedstrijd.Soort);
                            cmd.Parameters.AddWithValue("@weddatum", wedstrijd.Datum);
                            if (wedstrijd.Vereniging != null)
                            {
                                cmd.Parameters.AddWithValue("@wedvernr", wedstrijd.Vereniging.VerNr);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@wedvernr", 1034);
                            }

                            cmd.Connection = conn;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public Wedstrijd GetWedstrijdByDate(string date)
        {
            throw new NotImplementedException();
        }

        public Wedstrijd GetWedstrijdByName(string naam)
        {
            try
            {
                _verenigingLogic = new MysqlVerenigingLogic();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT WedID, WedNaam, WedSoort, WedDatum, VerNr " +
                                "FROM Wedstrijd Wed LEFT JOIN Vereniging Ver ON Ver.VerNr = Wed.WedVerNr " +
                                "WHERE WedNaam = @WedNaam;";

                            cmd.Parameters.AddWithValue("@WedNaam", naam);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int wedId = reader.GetInt32(0);
                                    string wedNaam = reader.GetString(1);
                                    Soort wedSoort = (Soort)Enum.Parse(typeof(Soort), reader.GetString(2));
                                    string wedDatum = reader.GetString(3);
                                    int verNr = reader.GetInt32(4);

                                    Vereniging vereniging = _verenigingLogic.GetVerenigingById(verNr);

                                    return new Wedstrijd(wedId, wedNaam, wedSoort, wedDatum, vereniging);
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public Wedstrijd GetWedstrijdById(int wedstrijdId)
        {
            try
            {
                _verenigingLogic = new MysqlVerenigingLogic();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {

                            cmd.CommandText =
                                "SELECT WedID, WedNaam, WedSoort, WedDatum, VerNr " +
                                "FROM Wedstrijd Wed LEFT JOIN Vereniging Ver ON Ver.VerNr = Wed.WedVerNr " +
                                "WHERE WedID = @wedId;";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijdId);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int wedId = reader.GetInt32(0);
                                    string wedNaam = reader.GetString(1);
                                    Soort wedSoort = (Soort)Enum.Parse(typeof(Soort), reader.GetString(2));
                                    string wedDatum = reader.GetString(3);
                                    int verNr = reader.GetInt32(4);

                                    Vereniging vereniging = _verenigingLogic.GetVerenigingById(verNr);

                                    return new Wedstrijd(wedId, wedNaam, wedSoort, wedDatum, vereniging);
                                }
                            }

                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }

        public List<Wedstrijd> ListWedstrijden()
        {
            try
            {
                _verenigingLogic = new MysqlVerenigingLogic();
                List<Wedstrijd> wedstrijden = new List<Wedstrijd>();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText =
                                    "SELECT WedID, WedNaam, WedSoort, WedDatum, VerNr, VerNaam, VerStraatNaam, VerHuisNr, VerPostcode, VerStad " +
                                    "FROM Wedstrijd Wed LEFT JOIN Vereniging Ver ON Ver.VerNr = Wed.WedVerNr;";
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string naam = reader.GetString(1);
                                    Soort soort = (Soort)Enum.Parse(typeof(Soort), reader.GetString(2));
                                    string datum = reader.GetString(3);
                                    int verNr = reader.GetInt32(4);
                                    Vereniging vereniging = _verenigingLogic.GetVerenigingById(verNr);

                                    wedstrijden.Add(new Wedstrijd(id, naam, soort, datum, vereniging));
                                }
                                return wedstrijden;
                            }
                        }
                    }
                }
                return null;
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public void RemoveWedstrijd(Wedstrijd wedstrijd)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "DELETE FROM Wedstrijd WHERE WedID = @wedId;";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijd.Id);

                            cmd.Connection = conn;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }
    }
}
