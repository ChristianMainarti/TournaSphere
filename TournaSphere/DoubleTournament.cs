namespace TournaSphere
{
    public class DoubleTournament : ITournament
    {
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Match> UpperBracket { get; private set; } = new List<Match>();
        public List<Match> LowerBracket { get; private set; } = new List<Match>();
        public List<Match> Matches { get; set; } = new List<Match>();
        public Match Final { get; private set; }

        public void AddTeam(string name)
        {
            if (Teams.Count % 2 != 0)
                throw new InvalidOperationException("The number of teams must be even.");

            Teams.Add(new Team { Id = Teams.Count + 1, Name = name });
        }

        public void GenerateUpperBracket()
        {
            if (Teams.Count % 2 != 0)
                throw new InvalidOperationException("The number of teams must be even.");

            var shuffledTeams = Teams.OrderBy(_ => Guid.NewGuid()).ToList();
            for (int i = 0; i < shuffledTeams.Count; i += 2)
            {
                UpperBracket.Add(new Match { TeamA = shuffledTeams[i], TeamB = shuffledTeams[i + 1] });
            }
        }

        public void DefineMatchResult(List<Match> matches, int index, int winner)
        {
            if (index < 0 || index >= matches.Count)
                throw new IndexOutOfRangeException("Match not found.");

            var match = matches[index];
            match.ScoreTeamA = winner == 1 ? 1 : 0;
            match.ScoreTeamB = winner == 2 ? 1 : 0;
        }

        public void GenerateNextRound()
        {
            var upperWinners = UpperBracket
                .Where(p => p.ScoreTeamA.HasValue && p.ScoreTeamB.HasValue)
                .Select(p => p.ScoreTeamA > p.ScoreTeamB ? p.TeamA : p.TeamB)
                .ToList();

            UpperBracket.Clear();
            for (int i = 0; i < upperWinners.Count; i += 2)
            {
                if (i + 1 < upperWinners.Count)
                    UpperBracket.Add(new Match { TeamA = upperWinners[i], TeamB = upperWinners[i + 1] });
            }

            if (UpperBracket.Count == 1 && LowerBracket.Count == 1)
            {
                Final = new Match
                {
                    TeamA = UpperBracket[0].ScoreTeamA > UpperBracket[0].ScoreTeamB ? UpperBracket[0].TeamA : UpperBracket[0].TeamB,
                    TeamB = LowerBracket[0].ScoreTeamA > LowerBracket[0].ScoreTeamB ? LowerBracket[0].TeamA : LowerBracket[0].TeamB
                };
            }
        }

        public List<Team> GetTeams()
        {
            return Teams;
        }

        public string GetType()
        {
            return "Elimination Tournament";
        }
    }
}
