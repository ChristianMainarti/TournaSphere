
namespace TournaSphere
{
    public class Match

    {
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }
        public int? ScoreTeamA { get; set; }
        public int? ScoreTeamB { get; set; }

        public void DefinirResultado(int ScoreA, int ScoreB)
        {
            ScoreTeamA = ScoreA;
            ScoreTeamB = ScoreB;

            if (ScoreA > ScoreB)
                TeamA.Points += 3; 
            else if (ScoreB > ScoreA)
                TeamB.Points += 3;
            else
            {
                TeamA.Points += 1; 
                TeamB.Points += 1;
            }

            TeamA.MatchesPlayed++;
            TeamB.MatchesPlayed++;
        }
    }
}

