namespace PokémonTeambuilder.core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
