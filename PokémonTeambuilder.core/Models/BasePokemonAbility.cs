namespace PokémonTeambuilder.core.Models
{
    public class BasePokemonAbility
    {
        public int BasePokemonId { get; set; }
        public BasePokemon BasePokemon { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }

        public bool IsHidden { get; set; }
        public int Slot { get; set; }
    }
}
