﻿namespace PokémonTeambuilder.core.Dto
{
    public class TypingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TypingDto> Weaknesses { get; set; }
        public ICollection<TypingDto> Resistances { get; set; }
        public ICollection<TypingDto> Immunities { get; set; }
        public ICollection<BasePokemonDto> BasePokemons { get; set; }
    }
}
