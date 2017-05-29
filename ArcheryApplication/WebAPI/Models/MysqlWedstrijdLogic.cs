using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArcheryApplication.Classes;
using ArcheryApplication.Storage;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebAPI.Models
{
    public class MysqlWedstrijdLogic : IWedstrijdServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        public List<Wedstrijd> GetAllWedstrijden()
        {
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
                                Vereniging vereniging = GetVerenigingById(verNr);

                                wedstrijden.Add(new Wedstrijd(id, naam, soort, datum, vereniging));
                            }
                            return wedstrijden;
                        }
                    }
                }
            }
            return null;
        }

        public Wedstrijd GetWedstrijdById(int id)
        {
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

                        cmd.Parameters.AddWithValue("@wedId", id);
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
                }
            }
            return null;
        }

        public Vereniging GetVerenigingById(int verNr)
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

                        cmd.Parameters.AddWithValue("@verNr", verNr);
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
    }
}