namespace PokémonTeambuilder.core.Dto
{
    public class PokemonDto
    {
        public int PokemonDtoId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }

        public BasePokemonDto BasePokemon { get; set; }
        public ItemDto Item { get; set; }
        public NatureDto Nature { get; set; }
        public AbilityDto Ability { get; set; }
        public ICollection<MoveDto> SelectedMoves { get; set; } // Many-to-many
        public StatsDto EVs { get; set; } // One-to-one or shared
        public int EVsId { get; set; }
        public StatsDto IVs { get; set; } // One-to-one or shared
        public int IVsId { get; set; }
    }
}
