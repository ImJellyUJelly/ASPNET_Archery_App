using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
{
    public class MysqlBaanindelingLogic : IBaanindelingServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        private MysqlWedstrijdLogic wedstrijdLogic;
        private MysqlVerenigingLogic verenigingLogic;
        private MysqlBaanLogic baanLogic;

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
                            cmd.CommandText = "SELECT DISTINCT BaanID, BaanNr, BaanLetter, Afstand, WedID, SchutID, SchutBondsNr, SchutNaam, RegDiscipline, SchutGeslacht, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Baanindeling baanindel " +
                                              "LEFT JOIN Wedstrijd W ON W.WedID = baanindel.BaIndelWedID " +
                                              "LEFT JOIN Registratie reg ON RegID = BaIndelSchutID " +
                                              "LEFT JOIN Schutter S ON S.SchutID = reg.RegSchutterID " +
                                              "LEFT JOIN Baan B ON B.BaanId = baanindel.BaIndelBaanID " +
                                              "LEFT JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "WHERE BaIndelWedID = @wedId " +
                                              "ORDER BY BaanNr, BaanLetter;";

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
                                    Geslacht _geslacht;
                                    string opmerking;
                                    Schutter schutter = null;
                                    if (!reader.IsDBNull(5))
                                    {
                                        schutid = reader.GetInt32(5);
                                        int schutbondsnr = reader.GetInt32(6);
                                        string schutnaam = reader.GetString(7);
                                        Discipline discipline = (Discipline)Enum.Parse(typeof(Discipline), reader.GetString(8));
                                        string geslacht = reader.GetString(9);
                                        if (geslacht == "M")
                                        {
                                            _geslacht = Geslacht.Heren;
                                        }
                                        else if (geslacht == "D")
                                        {
                                            _geslacht = Geslacht.Dames;
                                        }
                                        else // Default
                                        {
                                            _geslacht = Geslacht.Heren;
                                        }
                                        string email = reader.GetString(10);
                                        DateTime gebdatum = DateTime.Parse(reader.GetString(11));
                                        if (!reader.IsDBNull(12))
                                        {
                                            opmerking = reader.GetString(12);
                                        }
                                        else
                                        {
                                            opmerking = "";
                                        }
                                        Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), reader.GetString(13));
                                        Vereniging vereniging = wedstrijd.Vereniging;

                                        schutter = new Schutter(schutid, schutbondsnr, schutnaam, email, klasse, discipline, _geslacht, gebdatum, opmerking, vereniging);
                                    }
                                    Baan baan = new Baan(baanId, baanNr, baanLetter, afstand, wedstrijd);
                                    if (schutter != null)
                                    {
                                        baan.VoegSchutterToe(schutter);
                                    }
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

        public Baan GetBaanIdFromWedstrijd(int baanId, int wedstrijdId)
        {
            try
            {
                wedstrijdLogic = new MysqlWedstrijdLogic();
                verenigingLogic = new MysqlVerenigingLogic();

                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT BaanNr, BaanLetter, Afstand, VerNr, WedId " +
                                "FROM Baanindeling BI " +
                                "LEFT JOIN Baan B ON B.BaanID=BI.BaIndelBaanID " +
                                "LEFT JOIN Wedstrijd W ON W.WedID=BI.BaIndelWedID " +
                                "LEFT JOIN Vereniging V ON V.VerNr=W.WedVerNr " +
                                "WHERE BaIndelBaanID = @baanId AND BaIndelWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@baanId", baanId);
                            cmd.Parameters.AddWithValue("@wedId", wedstrijdId);

                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int baanNr = reader.GetInt32(0);
                                    string baanLetter = reader.GetString(1);
                                    int afstand = reader.GetInt32(2);
                                    Vereniging vereniging = verenigingLogic.GetVerenigingById(reader.GetInt32(3));
                                    Wedstrijd wedstrijd = wedstrijdLogic.GetWedstrijdById(reader.GetInt32(4));

                                    Baan baan = new Baan(baanId, baanNr, baanLetter, afstand, wedstrijd, vereniging);
                                    return baan;
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
                            cmd.CommandText = "INSERT INTO Baanindeling (BaIndelBaanId, BaIndelWedID, Afstand) VALUES (@baindelbaanid, @baindelwedid, @afstand);";

                            cmd.Parameters.AddWithValue("@baindelbaanid", baan.Id);
                            cmd.Parameters.AddWithValue("@baindelwedid", wedstrijdId);
                            cmd.Parameters.AddWithValue("@afstand", baan.Afstand);

                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();

                            conn.Close();
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }

        public void RemoveBanenFromWedstrijd(Wedstrijd wedstrijd, int baanid)
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
                                "DELETE FROM baanindeling WHERE BaIndelWedID = @wedstrijdId AND BaIndelBaanID = @baanId;";

                            cmd.Parameters.AddWithValue("@wedstrijdId", wedstrijd.Id);
                            cmd.Parameters.AddWithValue("@baanId", baanid);
                            cmd.Connection = conn;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            catch (DataException dex)
            {
                throw new DataException(dex.Message);
            }
        }

        public void AddSchutterToBaan(int wedId, int registratieId, int baanId)
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
                            cmd.CommandText = "UPDATE Baanindeling SET BaIndelSchutID = @schutterId WHERE BaIndelBaanID = @baanId AND BaIndelWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutterId", registratieId);
                            cmd.Parameters.AddWithValue("@baanId", baanId);

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

        public void VerwijderSchutterVanBaan(int wedId, int schutterId, int baanId)
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
                            cmd.CommandText = "UPDATE Baanindeling SET BaIndelSchutID = null WHERE BaIndelWedID = @wedId AND BaIndelBaanID = @baanId AND BaIndelSchutID = @schutterId;";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutterId", schutterId);
                            cmd.Parameters.AddWithValue("@baanId", baanId);

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

        public Schutter GetSchutterFromWedstrijd(int schutterId, int wedId)
        {
            try
            {
                verenigingLogic = new MysqlVerenigingLogic();
                baanLogic = new MysqlBaanLogic();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "SELECT SchutID, SchutBondsNr, SchutNaam, SchutGeslacht, RegDiscipline, SchutEmail, SchutGebDatum, SchutOpmerking, K.KlasseNaam, SchutVerNr, BaIndelBaanID " +
                            "FROM BaanIndeling BI " +
                            "LEFT JOIN Registratie R ON RegID = BaIndelSchutID " +
                            "LEFT JOIN Schutter S ON SchutID = RegSchutterID " +
                            "LEFT JOIN Klasse K ON K.KlasseID = SchutKlasseID " +
                            "WHERE BaIndelSchutID = @schutId AND BaIndelWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@schutId", schutterId);
                            cmd.Parameters.AddWithValue("@wedId", wedId);

                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    int bondsnr = reader.GetInt32(1);
                                    string naam = reader.GetString(2);
                                    Geslacht geslacht = GetSchutterGeslachtFromDB(reader.GetString(3));
                                    Discipline discipline =
                                        (Discipline)Enum.Parse(typeof(Discipline), reader.GetString(4));
                                    string email = reader.GetString(5);
                                    DateTime gebdatum = DateTime.Parse(reader.GetString(6));
                                    string opmerking;
                                    if (!reader.IsDBNull(7))
                                    {
                                        opmerking = reader.GetString(7);
                                    }
                                    else
                                    {
                                        opmerking = "";
                                    }
                                    Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), reader.GetString(8));
                                    Vereniging vereniging = verenigingLogic.GetVerenigingById(reader.GetInt32(9));
                                    int baanId = reader.GetInt32(10);

                                    Schutter schutter = new Schutter(id, bondsnr, naam, email, klasse, discipline,
                                        geslacht, gebdatum, opmerking, vereniging);
                                    Baan baan = GetBaanIdFromWedstrijd(baanId, wedId);
                                    baan.VoegSchutterToe(schutter);
                                    return schutter;
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
        private Geslacht GetSchutterGeslachtFromDB(string geslacht)
        {
            switch (geslacht)
            {
                case "M":
                    return Geslacht.Heren;
                case "D":
                    return Geslacht.Dames;
                default:
                    return Geslacht.Onzijdig;

            }
        }
    }
}