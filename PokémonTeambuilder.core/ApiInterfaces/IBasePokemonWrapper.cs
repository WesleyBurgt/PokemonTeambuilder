﻿using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface IBasePokemonWrapper
    {
        Task<List<BasePokemon>> GetPokemonList(int offset, int limit);
    }
}
