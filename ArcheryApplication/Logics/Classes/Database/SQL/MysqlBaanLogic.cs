using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using ArcheryApplication.Classes.Database.Interfaces;
using ArcheryApplication.Exceptions;

namespace ArcheryApplication.Classes.Database.SQL
{
    public class MysqlBaanLogic : IBaanServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";

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

                                        banen.Add(new Baan(baanid, baannummer, baanletter));
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
    }
}
