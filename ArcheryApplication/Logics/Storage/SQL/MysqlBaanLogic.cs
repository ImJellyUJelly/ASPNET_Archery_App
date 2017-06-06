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
        private MysqlVerenigingLogic _verenigingLogic;

        public Baan GetBaanById(int baanId)
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
                                "SELECT BaanId, BaanNr, BaaNletter, BaanVerNr " +
                                "FROM Baan " +
                                "LEFT JOIN Vereniging ON VerNr = BaanVerNr " +
                                "WHERE BaanId = @baanid;";

                            cmd.Parameters.AddWithValue("@baanid", baanId);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int baanid = reader.GetInt32(0);
                                    int baannr = reader.GetInt32(1);
                                    string baanletter = reader.GetString(2);
                                    int verNr = reader.GetInt32(3);

                                    Vereniging vereniging = _verenigingLogic.GetVerenigingById(verNr);
                                    Baan baan = new Baan(baanId, baannr, baanletter);
                                    baan.SetVereniging(vereniging);
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

        public Baan GetBaanByNummer(int baanNummer)
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
    }
}
