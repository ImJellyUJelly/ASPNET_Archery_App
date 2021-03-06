﻿using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
{
    public class MysqlVerenigingLogic : IVerenigingServices
    {
        private string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        public Vereniging GetVerenigingById(int verId)
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
                                "SELECT VerNr, VerNaam, VerStraatNaam, VerHuisNr, VerPostcode, VerStad FROM Vereniging WHERE VerNr = @verNr;";

                            cmd.Parameters.AddWithValue("@verNr", verId);
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

        public Vereniging GetVerenigingByName(string name)
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
                                "SELECT VerNr, VerNaam, VerStraatNaam, VerHuisNr, VerPostcode, VerStad FROM Vereniging WHERE VerNaam = @name;";

                            cmd.Parameters.AddWithValue("@name", name);
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

        public Vereniging GetVerenigingByNr(int verNr)
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

        public List<Baan> GetListBanen(int verNr)
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
                            cmd.CommandText = "SELECT BaanID, BaanNr, Baanletter FROM Baan WHERE BaanVerNr = @baanvernr;";

                            cmd.Parameters.AddWithValue("@baanvernr", verNr);

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
                    }
                }
                return null;
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
        }
    }
}
