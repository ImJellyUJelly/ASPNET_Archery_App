using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
{
    public class MysqlRegisterLogic : IRegistratieServices
    {
        private readonly string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        private MysqlVerenigingLogic verenigingLogic;
        private MysqlBaanLogic baanLogic;
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
                            cmd.CommandText = "";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijd.Id);
                            cmd.Connection = conn;


                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

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

        public int GetRegistratieId(int schutterId, int wedstrijdId)
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
                            cmd.CommandText = "SELECT RegID FROM Registratie WHERE RegSchutterID = @schutID AND RegWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@wedId", wedstrijdId);
                            cmd.Parameters.AddWithValue("@schutId", schutterId);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                return reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (NormalException ex)
            {
                throw new NormalException(ex.Message);
            }
            return 0;
        }

        public Schutter GetWedstrijdSchutterById(int wedId, int schutterId)
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
                            cmd.CommandText = "SELECT DISTINCT SchutID, SchutBondsNr, SchutNaam, SchutGeslacht, RegDiscipline, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Registratie R " +
                                              "LEFT JOIN Schutter S ON S.SchutID = R.RegSchutterID " +
                                              "LEFT JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "LEFT JOIN Vereniging V ON V.VerNr = S.SchutVerNr " +
                                              "WHERE RegSchutterID = @schutId AND RegWedID = @wedId;";

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

                                    Schutter schutter = new Schutter(id, bondsnr, naam, email, klasse, discipline,
                                        geslacht, gebdatum, opmerking, vereniging);
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

        public void SubscribeSchutterVoorWedstrijd(int wedId, int schutterId, string discipline)
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
                            cmd.CommandText = "INSERT INTO Registratie (RegSchutterID, RegWedID, RegDiscipline) VALUES (@schutterId, @wedId, @discipline);";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutterId", schutterId);
                            cmd.Parameters.AddWithValue("@discipline", discipline);

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

        public void UnsubscribeSchutterVoorWedstrijd(int wedId, int schutterId)
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
                            cmd.CommandText = "DELETE FROM Registratie WHERE RegWedID = @wedId AND RegSchutterID = @schutterId;";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutterId", schutterId);

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

        public void SetDisciplineFromSchutter(string discipline, int schutterId, int wedId)
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
                            cmd.CommandText = "UPDATE Registratie SET RegDiscipline = @discipline WHERE RegSchutterID = @schutterId AND RegWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@discipline", discipline);
                            cmd.Parameters.AddWithValue("@schutterId", schutterId);
                            cmd.Parameters.AddWithValue("@wedId", wedId);

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
