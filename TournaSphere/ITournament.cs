namespace TournaSphere
{
    public interface ITournament
    {
        void AddTeam(string name);
        List<Team> GetTeams();
        string GetType();
    }
}
