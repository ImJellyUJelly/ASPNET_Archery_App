namespace ArcheryApplication.Storage
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
