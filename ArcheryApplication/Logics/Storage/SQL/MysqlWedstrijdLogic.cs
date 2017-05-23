using System;
using System.Collections.Generic;
using System.Data;
using ArcheryApplication.Classes;
using MySql.Data.MySqlClient;

namespace ArcheryApplication.Storage
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
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
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
                                conn.Close();
                                return wedstrijden;
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
                            cmd.CommandText = "SELECT DISTINCT BaanID, BaanNr, BaanLetter, Afstand, WedID, SchutID, SchutBondsNr, SchutNaam, RegDiscipline, SchutGeslacht, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Baanindeling baanindel " +
                                              "LEFT JOIN Wedstrijd W ON W.WedID = baanindel.BaIndelWedID " +
                                              "LEFT JOIN Baan B ON B.BaanId = baanindel.BaIndelBaanID " +
                                              "LEFT JOIN Schutter S ON S.SchutID = baanindel.BaIndelSchutID " +
                                              "LEFT JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "LEFT JOIN Registratie reg ON reg.RegSchutterID = S.SchutID " +
                                              "WHERE BaIndelWedID = @wedId " +
                                              "ORDER BY WedId, BaanNr, BaanLetter;";

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
                                    int schutbondsnr;
                                    string schutnaam;
                                    Discipline discipline;
                                    Geslacht _geslacht;
                                    string email;
                                    DateTime gebdatum;
                                    string opmerking;
                                    Klasse klasse;
                                    Vereniging vereniging;
                                    Schutter schutter = null;
                                    if (!reader.IsDBNull(5))
                                    {
                                        schutid = reader.GetInt32(5);
                                        schutbondsnr = reader.GetInt32(6);
                                        schutnaam = reader.GetString(7);
                                        discipline = (Discipline)Enum.Parse(typeof(Discipline), reader.GetString(8));
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
                                        email = reader.GetString(10);
                                        gebdatum = DateTime.Parse(reader.GetString(11));
                                        if (!reader.IsDBNull(12))
                                        {
                                            opmerking = reader.GetString(12);
                                        }
                                        else
                                        {
                                            opmerking = "";
                                        }
                                        klasse = (Klasse)Enum.Parse(typeof(Klasse), reader.GetString(13));
                                        vereniging = wedstrijd.Vereniging;

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

        public void RemoveSchutterFromWedstrijd(int wedId, int schutterId)
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
                                "DELETE FROM Registratie WHERE RegWedID = @wedId AND RegSchutterID = @schutId;";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutId", schutterId);
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

        public Schutter GetSchutterById(int wedid, int schutid)
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
                            cmd.CommandText = "SELECT DISTINCT SchutID, SchutBondsNr, SchutNaam, SchutGeslacht, RegDiscipline, SchutEmail, SchutGebDatum, SchutOpmerking, KlasseNaam, SchutVerNr " +
                                              "FROM Registratie R " +
                                              "LEFT JOIN Schutter S ON S.SchutID = R.RegSchutterID " +
                                              "LEFT JOIN Klasse K ON K.KlasseID = S.SchutKlasseID " +
                                              "LEFT JOIN Vereniging V ON V.VerNr = S.SchutVerNr " +
                                              "WHERE SchutID = @schutId AND RegWedID = @wedId;";

                            cmd.Parameters.AddWithValue("@schutId", schutid);
                            cmd.Parameters.AddWithValue("@wedId", wedid);

                            cmd.Connection = conn;

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    int bondsnr = reader.GetInt32(1);
                                    string naam = reader.GetString(2);
                                    Geslacht geslacht = GeslachtCheck(reader.GetString(3));
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
                                    Vereniging vereniging = GetVerenigingById(reader.GetInt32(9));

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

        public void AddSchutterToBaan(int wedId, int schutterId, int baanId, int afstand)
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

        public void BewerkSchutterOpBaan(int wedId, int schutterId, int baanId)
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
                            cmd.CommandText = "";

                            cmd.Parameters.AddWithValue("@wedId", wedId);
                            cmd.Parameters.AddWithValue("@schutterId", schutterId);
                            cmd.Parameters.AddWithValue("@baanid", baanId);

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

        private Geslacht GeslachtCheck(string geslacht)
        {
            if (geslacht == "M")
            {
                return Geslacht.Heren;
            }
            else if (geslacht == "D")
            {
                return Geslacht.Dames;
            }
            else
            {
                return Geslacht.Onzijdig;
            }
        }
    }
}
