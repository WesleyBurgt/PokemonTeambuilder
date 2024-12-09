﻿namespace PokémonTeambuilder.DTOs
{
    public class PokemonDto : BasePokemonDto
    {
        public int PersonalId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public ItemDto? Item { get; set; }
        public NatureDto Nature { get; set; }
        public AbilityDto Ability { get; set; }
        public List<MoveDto> SelectedMoves { get; set; }
        public StatsDto EVs { get; set; }
        public StatsDto IVs { get; set; }
    }
}
