using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public class MysqlBaanLogic : IBaanServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        private MysqlWedstrijdLogic wedstrijdLogic = new MysqlWedstrijdLogic();
        public List<Baan> ListBanen(int VerNr)
        {
            try
            {
                List<Baan> banen = new List<Baan>();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            try
                            {
                                cmd.CommandText = "SELECT BaanID, BaanNr, Baanletter FROM Baan WHERE BaanVerNr = @baanvernr;";

                                cmd.Parameters.AddWithValue("@baanvernr", VerNr);

                                cmd.Connection = conn;

                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int baanid = reader.GetInt32(0);
                                        int baannummer = reader.GetInt32(1);
                                        string baanletter = reader.GetString(2);

                                        banen.Add(new Baan(baanid, baannummer, baanletter, 70));
                                    }
                                    return banen;
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

        public Baan GetBaanById(int baanId)
        {
            throw new NotImplementedException();
        }

        public Baan GetBaanByNummer(int baanNummer)
        {
            throw new NotImplementedException();
        }

        public List<Schutter> ListSchuttersOpBaan(int baanId)
        {
            throw new NotImplementedException();
        }

        public void AddBaan(Baan baan)
        {
            throw new NotImplementedException();
        }

        public void EditBaan(Baan baan)
        {
            throw new NotImplementedException();
        }

        public void RemoveBaan(Baan baan)
        {
            throw new NotImplementedException();
        }

        public void AddSchutterTobaan(Schutter schutter, int baanId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSchutterFromBaan(Schutter schutter, int baanId)
        {
            throw new NotImplementedException();
        }

        public Baan GetBaanIdFromWedstrijd(int baanId, int wedId)
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
                                "SELECT BaanID, BaanNr, BaanLetter, Afstand, BaanVerNr, WedID, WedNaam " +
                                "FROM Baanindeling BI " +
                                "LEFT JOIN Baan B ON B.BaanID = BI.BaIndelBaanID " +
                                "LEFT JOIN Wedstrijd W ON W.WedID = BI.BaIndelWedID " +
                                "WHERE BaIndelBaanID = @baanId AND BaIndelWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@baanId", baanId);
                            cmd.Parameters.AddWithValue("@wedId", wedId);

                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int baanID = reader.GetInt32(0);
                                    int baanNr = reader.GetInt32(1);
                                    string baanLetter = reader.GetString(2);
                                    int afstand = reader.GetInt32(3);
                                    Vereniging vereniging = wedstrijdLogic.GetVerenigingById(reader.GetInt32(4));
                                    Wedstrijd wedstrijd = wedstrijdLogic.GetWedstrijdById(reader.GetInt32(5));

                                    Baan baan = new Baan(baanID, baanNr, baanLetter, afstand, wedstrijd, vereniging);
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
    }
}
