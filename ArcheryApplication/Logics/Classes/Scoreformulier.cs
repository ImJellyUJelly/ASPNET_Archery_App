using System.Collections.Generic;

namespace ArcheryApplication.Classes
{
    public class Scoreformulier
    {
        public List<Score> ScorePerAfstand { get; private set; } // 1 score is het aantal pijlen dat op 1 afstand geschoten wordt (bijv. 36 pijlen op 70m. = max. 360 punten)
        public int TotaalScore { get; private set; } // de scores van alle afstanden opgeteld
        public Scoreformulier()
        {
            ScorePerAfstand = new List<Score>();
            TotaalScore = 0;
        }

        public int GetScores()
        {
            return TotaalScore;
        }

        //update
        public bool AddScore(Score score)
        {
            try
            {
                ScorePerAfstand.Add(score);
                BerekenTotaalScore(ScorePerAfstand);
                return true;
            }
            catch (ScoreException sex)
            {
                throw sex;
            }
        }

        public bool ChangeScore(Score score)
        {
            try
            {
                foreach (Score s in ScorePerAfstand)
                {
                    if (s.Id == score.Id)
                    {
                        s.ChangeScore(score.AantalPunten);
                        s.ChangeAfstand(score.Afstand);
                        BerekenTotaalScore(ScorePerAfstand);

                        return true;
                    }
                }
            }
            catch (ScoreException sex)
            {
                throw sex;
            }
            return false;
        }
        public bool RemoveScore(Score score)
        {
            ScorePerAfstand.Remove(score);
            BerekenTotaalScore(ScorePerAfstand);
            return true;
        }
        private void BerekenTotaalScore(List<Score> scores)
        {
            int totaal = 0;
            foreach (Score s in scores)
            {
                totaal += s.AantalPunten;
            }
            TotaalScore = totaal;
        }
    }
}
