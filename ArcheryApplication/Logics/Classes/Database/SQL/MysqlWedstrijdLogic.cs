using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes.Database.Interfaces;
using ArcheryApplication.Classes.Enums;
using ArcheryApplication.Exceptions;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Classes.Database.SQL
{
    public class MysqlWedstrijdLogic : IWedstrijdServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";

        public void AddBaanToWedstrijd(Baan baan, int wedstrijdId)
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
                            try
                            {
                                cmd.CommandText = "INSERT INTO Baanindeling (BaIndelBaanId, BaIndelWedID, Afstand) VALUES (@baindelbaanid, @baindelwedid, @afstand);";

                                cmd.Parameters.AddWithValue("@baindelbaanid", baan.Id);
                                cmd.Parameters.AddWithValue("@baindelwedid", wedstrijdId);
                                cmd.Parameters.AddWithValue("@afstand", baan.Afstand);

                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

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
                            try
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
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public void EditBaanFromWedstrijd(Baan baan, int wedstrijdId)
        {
            throw new NotImplementedException();
        }

        public void EditWedstrijd(Wedstrijd wedstrijd)
        {
            throw new NotImplementedException();
        }

        public Wedstrijd GetWedstrijdByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Wedstrijd GetWedstrijdByName(string naam)
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
                            try
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

                                        Vereniging vereniging = GetVerenigingById(verNr);

                                        return new Wedstrijd(wedId, wedNaam, wedSoort, wedDatum, vereniging);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
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
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            try
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

                                        Vereniging vereniging = GetVerenigingById(verNr);

                                        return new Wedstrijd(wedId, wedNaam, wedSoort, wedDatum, vereniging);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
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

        public List<Wedstrijd> ListWedstrijden()
        {
            try
            {
                List<Wedstrijd> wedstrijden = new List<Wedstrijd>();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            try
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
                                        string naam;
                                        // Volgens mij hoeft dit niet eens, naam is namelijk verplicht.
                                        if (!reader.IsDBNull(1))
                                        {
                                            naam = reader.GetString(1);
                                        }
                                        else
                                        {
                                            naam = "Geen naam";
                                        }
                                        Soort soort = (Soort)Enum.Parse(typeof(Soort), reader.GetString(2));
                                        string datum = reader.GetString(3);
                                        int verNr = reader.GetInt32(4);
                                        // Vereniging

                                        Vereniging vereniging = GetVerenigingById(verNr);

                                        wedstrijden.Add(new Wedstrijd(id, naam, soort, datum, vereniging));
                                    }
                                    return wedstrijden;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
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

        public void RemoveBaanFromWedstrijd(Baan baan, int wedstrijdId)
        {
            throw new NotImplementedException();
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
                            try
                            {
                                cmd.CommandText = "DELETE FROM Wedstrijd WHERE WedID = @wedId;";

                                cmd.Parameters.AddWithValue("@wedId", wedstrijd.Id);

                                cmd.Connection = conn;

                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new DataException(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public List<Baan> GetWedstrijdBanen(Wedstrijd wedstrijd)
        {
            List<Baan> banen = new List<Baan>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "SELECT BaanID, BaanNr, BaanLetter, Afstand, WedID, SchutID, SchutBondsNr, SchutNaam, RegDiscipline, SchutGeslacht, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Baanindeling baanindel " +
                                              "LEFT JOIN Wedstrijd W ON W.WedID = baanindel.BaIndelWedID " +
                                              "LEFT JOIN Baan B ON B.BaanId = baanindel.BaIndelBaanID " +
                                              "LEFT JOIN Schutter S ON S.SchutID = baanindel.BaIndelSchutID " +
                                              "LEFT JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "LEFT JOIN Registratie reg ON reg.RegSchutterID = S.SchutID " +
                                              "WHERE WedID = 1;";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijd.Id);
                            cmd.Connection = conn;


                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int baanId = reader.GetInt32(0);
                                    int baanNr = reader.GetInt32(1);
                                    string baanLetter = reader.GetString(2);
                                    int afstand;
                                    if (!reader.IsDBNull(3))
                                    {
                                        afstand = reader.GetInt32(3);
                                    }
                                    else
                                    {
                                        afstand = 0;
                                    }
                                    int schutid;
                                    if (!reader.IsDBNull(5))
                                    {
                                        schutid = reader.GetInt32(5);
                                    }
                                    else
                                    {
                                        schutid = -1;
                                        Schutter geenSchutter = null;
                                    }
                                    int schutbondsnr = reader.GetInt32(6);
                                    string schutnaam = reader.GetString(7);
                                    Discipline discipline = (Discipline)Enum.Parse(typeof(Discipline), reader.GetString(8));
                                    Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), reader.GetString(9));
                                    string email = reader.GetString(10);
                                    DateTime gebdatum = DateTime.Parse(reader.GetString(11));
                                    string opmerking = reader.GetString(12);
                                    Klasse klasse = (Klasse) Enum.Parse(typeof(Klasse), reader.GetString(13));
                                    Vereniging vereniging = wedstrijd.Vereniging;

                                    Schutter schutter = new Schutter(schutid, schutbondsnr, schutnaam, email, klasse, discipline, geslacht, gebdatum, opmerking, vereniging);
                                    Baan baan = new Baan(baanId, baanNr, baanLetter, afstand, wedstrijd);

                                    baan.VoegSchutterToe(schutter);
                                    banen.Add(baan);
                                }
                                if (banen.Count >= 1)
                                {
                                    return banen;
                                }
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
            return null;
        }

        public Vereniging GetVerenigingById(int verNr)
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
                            cmd.CommandText =
                                "SELECT VerNr, VerNaam, VerStraatNaam, VerHuisNr, VerPostcode, VerStad FROM Vereniging WHERE VerNr = @vernr;";

                            cmd.Parameters.AddWithValue("@vernr", verNr);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int vernr = reader.GetInt32(0);
                                    string vernaam = reader.GetString(1);
                                    string verstraat = reader.GetString(2);
                                    string verhuisnr = reader.GetString(3);
                                    string verpostcode = reader.GetString(4);
                                    string verstad = reader.GetString(5);

                                    return new Vereniging(vernr, vernaam, verstraat, verhuisnr, verpostcode, verstad);
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (DataException dex)
            {
                throw new DataException(dex.Message);
            }
        }

        public List<Schutter> GetWedstrijdSchutters(Wedstrijd wedstrijd)
        {
            List<Schutter> schutters = new List<Schutter>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "SELECT BaanID, BaanNr, BaanLetter, WedID " +
                                              "FROM Baanindeling BaanInd " +
                                              "LEFT JOIN Wedstrijd Wed ON Wed.WedID = BaanInd.BaIndelWedID " +
                                              "LEFT JOIN Baan ba ON ba.BaanID = BaanInd.BaIndelBaanID " +
                                              "WHERE WedID = @wedId;";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijd.Id);
                            cmd.Connection = conn;


                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {


                                    //schutters.Add(new Schutter());
                                }
                                if (schutters.Count >= 1)
                                {
                                    return schutters;
                                }
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
            return null;
        }
    }
}
