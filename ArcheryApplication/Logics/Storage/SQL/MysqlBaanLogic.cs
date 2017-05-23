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

        public Baan GetBaanById(int baanId)
        {
            throw new NotImplementedException();
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
