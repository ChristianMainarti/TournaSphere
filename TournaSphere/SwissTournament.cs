namespace TournaSphere
{
    public class SwissTournament : ITournament
    {
        public List<Team> Teams { get; private set; } = new List<Team>();
        public List<Match> Matches { get; set; } = new List<Match>();

        public void AddTeam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The team name cannot be empty or null.");
            }

            if (Teams.Any(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"The team '{name}' has already been added.");
            }

            int newId = Teams.Count + 1;
            Teams.Add(new Team { Id = newId, Name = name });
        }

        public List<Team> GetTeams()
        {
            return Teams;
        }

        public string GetType()
        {
            return "Swiss Bracket";
        }

        public void GenerateRound()
        {
            if (Teams.Count < 2)
            {
                throw new InvalidOperationException("At least 2 teams are required to generate a round.");
            }

            var orderedTeams = Teams
                .OrderByDescending(t => t.Points)
                .ThenBy(t => t.MatchesPlayed)
                .ToList();

            var rounds = new List<Match>();

            for (int i = 0; i < orderedTeams.Count; i++)
            {
                for (int j = i + 1; j < orderedTeams.Count; j++)
                {
                    var teamA = orderedTeams[i];
                    var teamB = orderedTeams[j];

                    if (!MatchExists(teamA, teamB))
                    {
                        rounds.Add(new Match { TeamA = teamA, TeamB = teamB });
                        break;
                    }
                }
            }

            if (rounds.Count == 0)
            {
                throw new InvalidOperationException("No more matches can be generated.");
            }

            Matches.AddRange(rounds);
        }

        private bool MatchExists(Team teamA, Team teamB)
        {
            return Matches.Any(m =>
                (m.TeamA == teamA && m.TeamB == teamB) ||
                (m.TeamA == teamB && m.TeamB == teamA));
        }
    }
}