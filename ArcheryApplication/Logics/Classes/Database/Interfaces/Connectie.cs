using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryApplication.Classes.Database.Interfaces
{
    public class Connectie
    {
        public string ConnectieString;

        public Connectie()
        {
            ConnectieString = @"Server=studmysql01.fhict.local;Uid=dbi299244;Database=dbi299244;Pwd=yourPassword;";
        }

        public void SetConnectieString(string nieuweString)
        {
            ConnectieString = nieuweString;
        }

        public string GetConnectieString()
        {
            return ConnectieString;
        }
    }
}
