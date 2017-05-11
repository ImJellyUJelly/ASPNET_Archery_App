namespace Model
{
    public class Vereniging
    {
        public int VerNr { get; private set; }
        public string Naam { get; private set; }
        public string Straatnaam { get; private set; }
        public string Huisnummer { get; private set; }
        public string Postcode { get; private set; }
        public string Plaats { get; private set; }

        public Vereniging(int nr, string naam, string straat, string hnr, string postcode, string plaats)
        {
            VerNr = nr;
            Naam = naam;
            Straatnaam = straat;
            Huisnummer = hnr;
            Postcode = postcode;
            Plaats = plaats;
        }
        public Vereniging(int nr, string naam)
        {
            VerNr = nr;
            Naam = naam;
        }
        public Vereniging(string naam)
        {
            Naam = naam;
        }
        //change
        public bool ChangeNr(int nr)
        {
            VerNr = nr;
            return true;
        }
        public bool ChangeNaam(string naam)
        {
            Naam = naam;
            return true;
        }
        public bool ChangePlaats(string plaats)
        {
            Plaats = plaats;
            return true;
        }
    }
}
