namespace TournaSphere
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; } = 0;
        public int MatchesPlayed { get; set; } = 0;
    }
}
