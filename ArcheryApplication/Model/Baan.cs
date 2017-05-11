using Model.Exceptions;

namespace Model
{ 
    public class Baan
    {
        public int Id { get; private set; }
        public string BaanId { get; private set; }
        public int Baannummer { get; private set; }
        public string Letter { get; private set; }
        public Schutter Schutter { get; private set; }
        public int Afstand { get; private set; }

        public Baan(int baanNummer, string letter, int afstand)
        {
            Baannummer = baanNummer;
            Letter = letter;
            Afstand = afstand;
            CreeerBaanId();
        }
        public Baan(int id, int baannummer, string letter, int afstand)
        {
            Id = id;
            Baannummer = baannummer;
            Letter = letter;
            Afstand = afstand;
            CreeerBaanId();
        }
        public void VoegSchutterToe(Schutter schutter)
        {
            if (Schutter == null)
            {
                Schutter = schutter;
                schutter.GeefSchutterEenBaan(this);   
            }
        }
        public void VerwijderSchutter(Schutter schutter)
        {
            //ToDo
        }

        private void CreeerBaanId()
        {
            BaanId = Baannummer.ToString() + Letter;
        }
        public override string ToString()
        {
            if (Schutter != null)
            {
                return $"{ BaanId }: { Schutter.ToString() }";
            }
            return $"{ BaanId }: Geen schutter";

        }
    }
}
