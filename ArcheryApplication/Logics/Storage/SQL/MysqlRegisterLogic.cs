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

        public Schutter GetWedstrijdSchutterById(int wedId, int schutterId)
        {
            throw new NotImplementedException();
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
    }
}
