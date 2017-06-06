namespace ArcheryApplication.Classes
{ 
    public class Baan
    {
        public int Id { get; private set; }
        public string BaanId { get; private set; }
        public int Baannummer { get; private set; }
        public string Letter { get; private set; }
        public Schutter Schutter { get; private set; }
        public int Afstand { get; private set; }
        public Wedstrijd Wedstrijd { get; private set; }
        public Vereniging Vereniging { get; private set; }

        public Baan(int id, int nummer, string letter)
        {
            Id = id;
            Baannummer = nummer;
            Letter = letter;
        }

        public Baan(int baanNummer, string letter, int afstand)
        {
            Baannummer = baanNummer;
            Letter = letter;
            Afstand = afstand;
            CreeerBaanId();
        }
        public Baan(int id, int baannummer, string letter, int afstand, Wedstrijd wedstrijd) : this(id, baannummer, letter)
        {
            Afstand = afstand;
            Wedstrijd = wedstrijd;
            CreeerBaanId();
        }
        public Baan(int id, int baannummer, string letter, int afstand) : this(id, baannummer, letter)
        {
            Afstand = afstand;
            CreeerBaanId();
        }

        public Baan(int id, int baannummer, string letter, int afstand, Wedstrijd wedstrijd, Vereniging vereniging)
            : this(id, baannummer, letter, afstand, wedstrijd)
        {
            Vereniging = vereniging;
        }

        public void SetAfstand(int afstand)
        {
            Afstand = afstand;
        }

        public void SetVereniging(Vereniging vereniging)
        {
            Vereniging = vereniging;
        }

        public void VoegSchutterToe(Schutter schutter)
        {
            if (Schutter == null)
            {
                Schutter = schutter;
                schutter.GeefSchutterEenBaan(this);   
            }
        }
        public void VerwijderSchutter()
        {
            if (Schutter != null)
            {
                Schutter = null;
            }
        }

        private void CreeerBaanId()
        {
            BaanId = Baannummer + Letter;
        }
        public override string ToString()
        {
            if (Schutter != null)
            {
                return $"{ BaanId }: { Schutter }";
            }
            return $"{ BaanId }: Geen schutter";

        }
    }
}
