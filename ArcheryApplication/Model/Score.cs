using Model.Exceptions;

namespace Model
{
    public class Score
    {
        public int Id { get; private set; }
        public int AantalPunten { get; private set; }
        public int Afstand { get; private set; }
        // Algemene constructor
        public Score(int score, int afstand)
        {
            AddScore(score, afstand);
        }
        // Algemene constructor met ID (voor Database)
        public Score(int id, int score, int afstand) : this(score, afstand)
        {
            Id = id;
        }

        public bool AddScore(int score, int afstand)
        {
            if (score > 360)
            {
                throw new ScoreException("Score kan niet hoger zijn dan 360 punten.");
            }
            AantalPunten = score;
            Afstand = afstand;
            return true;
        }
        //change
        public bool ChangeScore(int score)
        {
            int previousscore = 0;
            previousscore = AantalPunten;
            AantalPunten = score;
            if (AantalPunten > 360)
            {
                AantalPunten = previousscore;
                throw new ScoreException("Score kan niet hoger zijn dan 360 punten.");
            }
            return true;
        }
        public bool ChangeAfstand(int afstand)
        {
            Afstand = afstand;
            return true;
        }
        public override string ToString()
        {
            return Afstand.ToString() + " " + AantalPunten.ToString();
        }
    }
}
