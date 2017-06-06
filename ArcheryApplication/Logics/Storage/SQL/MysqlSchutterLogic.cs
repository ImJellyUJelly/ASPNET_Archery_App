using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
{
    public class MysqlSchutterLogic : ISchutterServices
    {
        private const string _connectie = "Server = studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=Geschiedenis1500;";
        private MysqlVerenigingLogic verenigingLogic;
        public Schutter GetSchutterById(int schutterId)
        {
            try
            {
                verenigingLogic = new MysqlVerenigingLogic();
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
                                              "WHERE SchutID = @schutId;";

                            cmd.Parameters.AddWithValue("@schutId", schutterId);

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

        public Schutter GetSchutterByBondsNr(int bondsnummer)
        {
            try
            {
                verenigingLogic = new MysqlVerenigingLogic();
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
                                              "WHERE SchutBondsNr = @schutBondsNr;";

                            cmd.Parameters.AddWithValue("@schutBondsNr", bondsnummer);

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

        public List<Schutter> ListSchutters()
        {
            try
            {
                verenigingLogic = new MysqlVerenigingLogic();
                List<Schutter> schutters = new List<Schutter>();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT SchutID, SchutBondsNr, SchutNaam, SchutGeslacht, RegDiscipline, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                "FROM Schutter S " +
                                "LEFT JOIN Registratie R ON R.RegSchutterID=S.SchutID " +
                                "LEFT JOIN Klasse K ON K.KlasseID=S.SchutKlasseID;";
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

                                    schutters.Add(schutter);
                                }
                                return schutters;
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

        public void AddSchutter(Schutter schutter)
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
                                "INSERT INTO Schutter (SchutBondsNr, SchutNaam, SchutGeslacht, SchutEmail, SchutGebDatum, SchutOpmerking, SchutKlasseID, SchutVerNr) " +
                                "VALUES (@schutterBondsnr, @schutterNaam, @schutterGeslacht, @schutterEmail, @schutterGebdatum, @schutterOpmerking, @schutterKlasse, @schutterVerNr);";

                            cmd.Parameters.AddWithValue("@schutterBondsnr", schutter.Bondsnummer);
                            cmd.Parameters.AddWithValue("@schutterNaam", schutter.Naam);
                            cmd.Parameters.AddWithValue("@schutterKlasse", GetKlasseId(schutter.Klasse));
                            cmd.Parameters.AddWithValue("@schutterEmail", schutter.Emailadres);
                            cmd.Parameters.AddWithValue("@schutterGeslacht", GetSchutterGeslacht(schutter.Geslacht));
                            cmd.Parameters.AddWithValue("@schutterOpmerking", schutter.Opmerking);
                            cmd.Parameters.AddWithValue("@schutterGebdatum", schutter.Geboortedatum);
                            cmd.Parameters.AddWithValue("@schutterVerNr", schutter.Vereniging.VerNr);
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

        public void EditSchutter(Schutter schutter)
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
                                "UPDATE Schutter SET SchutBondsNr = @schutterBondsnr, SchutNaam = @schutterNaam, SchutGeslacht = @schutterGeslacht, " +
                                "SchutEmail = @schutterEmail, SchutGebDatum = @schutterGebdatum, SchutOpmerking = @schutterOpmerking, " +
                                "SchutKlasseId = @schutterKlasse, SchutVerNr = @schutterVerNr " +
                                "WHERE SchutID = @schutterId;";

                            cmd.Parameters.AddWithValue("@schutterId", schutter.Id);
                            cmd.Parameters.AddWithValue("@schutterBondsnr", schutter.Bondsnummer);
                            cmd.Parameters.AddWithValue("@schutterNaam", schutter.Naam);
                            cmd.Parameters.AddWithValue("@schutterKlasse", GetKlasseId(schutter.Klasse));
                            cmd.Parameters.AddWithValue("@schutterEmail", schutter.Emailadres);
                            cmd.Parameters.AddWithValue("@schutterGeslacht", GetSchutterGeslacht(schutter.Geslacht));
                            cmd.Parameters.AddWithValue("@schutterOpmerking", schutter.Opmerking);
                            cmd.Parameters.AddWithValue("@schutterGebdatum", schutter.Geboortedatum);
                            cmd.Parameters.AddWithValue("@schutterVerNr", schutter.Vereniging.VerNr);
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

        public void RemoveSchutter(Schutter schutter)
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
                                "DELETE FROM Schutter WHERE SchutID = @schutterId;";

                            cmd.Parameters.AddWithValue("@schutterId", schutter.Id);
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

        public Schutter GetSchutterByNameAndBondsNr(int bondsnr, string naam)
        {
            try
            {
                verenigingLogic = new MysqlVerenigingLogic();
                using (MySqlConnection conn = new MySqlConnection(_connectie))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "SELECT DISTINCT SchutID, SchutBondsNr, SchutNaam, SchutGeslacht, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Schutter S " +
                                              "INNER JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "INNER JOIN Vereniging V ON V.VerNr = S.SchutVerNr " +
                                              "WHERE SchutNaam = @naam AND SchutBondsNr = @bondsnr;";

                            cmd.Parameters.AddWithValue("@bondsnr", bondsnr);
                            cmd.Parameters.AddWithValue("@naam", naam);
                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string schutnaam;
                                    if (!reader.IsDBNull(2))
                                    {
                                        int id = reader.GetInt32(0);
                                        int bondsnummer = reader.GetInt32(1);
                                        schutnaam = reader.GetString(2);
                                        Geslacht geslacht = GetSchutterGeslachtFromDB(reader.GetString(3));
                                        string email = reader.GetString(4);
                                        DateTime gebdatum = DateTime.Parse(reader.GetString(5));
                                        string opmerking;
                                        if (!reader.IsDBNull(6))
                                        {
                                            opmerking = reader.GetString(6);
                                        }
                                        else
                                        {
                                            opmerking = "";
                                        }
                                        Klasse klasse = (Klasse)Enum.Parse(typeof(Klasse), reader.GetString(7));
                                        Vereniging vereniging = verenigingLogic.GetVerenigingByNr(reader.GetInt32(8));

                                        Schutter schutter = new Schutter(id, bondsnummer, schutnaam, email, klasse, geslacht, gebdatum, opmerking, vereniging);
                                        return schutter;
                                    }
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

        private int GetKlasseId(Klasse klasse)
        {
            switch (klasse)
            {
                case Klasse.Aspirant:
                    return 1;

                case Klasse.Cadet:
                    return 2;

                case Klasse.Junior:
                    return 3;

                case Klasse.Senior:
                    return 4;

                case Klasse.Veteraan:
                    return 5;

                default:
                    return 4;
            }
        }

        private string GetSchutterGeslacht(Geslacht geslacht)
        {
            switch (geslacht)
            {
                case Geslacht.Heren:
                    return "M";
                case Geslacht.Dames:
                    return "D";
                case Geslacht.Onzijdig:
                    return "O";
                default:
                    return "O";
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
    }
}
