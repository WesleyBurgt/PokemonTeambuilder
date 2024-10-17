using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Dto;

namespace PokémonTeambuilder.core.Services
{
    public class BasePokemonService
    {
        private readonly IBasePokemonWrapper pokemonWrapper;
        private readonly IBasePokemonRepos pokemonRepos;

        public BasePokemonService(IBasePokemonWrapper pokemonWrapper)
        {
            this.pokemonWrapper = pokemonWrapper;
        }
        public BasePokemonService(IBasePokemonRepos pokemonRepos) 
        {
            this.pokemonRepos = pokemonRepos;
        }

        public async Task<List<BasePokemon>> GetPokemonListFromApi(int offset, int limit)
        {
            if (pokemonWrapper == null)
            {
                throw new Exception("pokemonWrapper cannot be null"); //TODO: make custom Exception
            }

            List<BasePokemon> list = await pokemonWrapper.GetPokemonList(offset, limit);
            foreach (BasePokemon pokemon in list)
            {
                ValidateBasePokemon(pokemon);
            }
            return list;
        }

        public async Task<List<BasePokemon>> GetPokemonList(int offset, int limit)
        {
            if(pokemonRepos == null)
            {
                throw new Exception("pokemonRepos cannot be null"); //TODO: make custom Exception
            }

            List<BasePokemonDto> list = [];
            try
            {
                list = await pokemonRepos.GetBasePokemonList(offset, limit);
            }
            catch (Exception ex) 
            {
                return [];
            }

            List<BasePokemon> result = [];

            foreach (BasePokemonDto pokemon in list)
            {
                List<Ability> abilities = [];
                List<Move> moves = [];
                List<Typing> typings = [];
                foreach(AbilityDto ability in pokemon.Abilities)
                {
                    abilities.Add(MapAbilityDtoToAbility(ability));
                }
                foreach (MoveDto move in pokemon.Moves)
                {
                    moves.Add(MapMoveDtoToMove(move));
                }
                foreach (TypingDto typing in pokemon.Typings)
                {
                    typings.Add(MapTypingDtoToTyping(typing));
                }

                result.Add(new BasePokemon
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Abilities = abilities,
                    BaseStats = MapStatsDtoToStats(pokemon.BaseStats),
                    Moves = moves,
                    Typings = typings,
                    Sprite = pokemon.Sprite
                });
            }
            foreach(BasePokemon basePokemon in result)
            {
                ValidateBasePokemon(basePokemon);
            }
            return result;
        }

        private void ValidateBasePokemon(BasePokemon pokemon)
        {
            //TODO: make custom Exceptions
            if (pokemon.Id <= 0)
                throw new Exception("Pokemon Id cannot be" + pokemon.Id);
            if (String.IsNullOrEmpty(pokemon.Name))
                throw new Exception("Pokemon Name cannot be null or empty");
            if (pokemon.Typings.Count <= 0)
                throw new Exception("Pokemon cannot have no typing");
            if (pokemon.BaseStats.Hp <= 0)
                throw new Exception("Pokemon Hp cannot be" + pokemon.BaseStats.Hp);
            if (pokemon.BaseStats.Attack <= 0)
                throw new Exception("Pokemon Attack cannot be" + pokemon.BaseStats.Attack);
            if (pokemon.BaseStats.Defense <= 0)
                throw new Exception("Pokemon Defense cannot be" + pokemon.BaseStats.Defense);
            if (pokemon.BaseStats.SpecialAttack <= 0)
                throw new Exception("Pokemon SpecialAttack cannot be" + pokemon.BaseStats.SpecialAttack);
            if (pokemon.BaseStats.SpecialDefense <= 0)
                throw new Exception("Pokemon SpecialDefense cannot be" + pokemon.BaseStats.SpecialDefense);
            if (pokemon.BaseStats.Speed <= 0)
                throw new Exception("Pokemon Speed cannot be" + pokemon.BaseStats.Speed);
            if (pokemon.Abilities.Count <= 0)
                throw new Exception("Pokemon cannot have no abilities");
        }

        private Ability MapAbilityDtoToAbility(AbilityDto abilityDto)
        {
            return new Ability
            {
                Id = abilityDto.Id,
                Name = abilityDto.Name,
                Description = abilityDto.Description,
                IsHidden = abilityDto.IsHidden,
                Slot = abilityDto.Slot
            };
        }

        private Stats MapStatsDtoToStats(StatsDto statsDto)
        {
            return new Stats
            {
                Hp = statsDto.Hp,
                Attack = statsDto.Attack,
                Defense = statsDto.Defense,
                SpecialAttack = statsDto.SpecialAttack,
                SpecialDefense = statsDto.SpecialDefense,
                Speed = statsDto.Speed
            };
        }

        private Move MapMoveDtoToMove(MoveDto moveDto)
        {
            return new Move
            {
                Id = moveDto.Id,
                Name = moveDto.Name,
                Accuracy = moveDto.Accuracy,
                BasePower = moveDto.BasePower,
                Description = moveDto.Description,
                Typing = MapTypingDtoToTyping(moveDto.Typing),
            };
        }

        private Typing MapTypingDtoToTyping(TypingDto typingDto)
        {
            List<TypingRelationless> weaknesses = [];
            List<TypingRelationless> resistances = [];
            List<TypingRelationless> immunities = [];

            foreach (TypingDto weakness in typingDto.Weaknesses)
            {
                weaknesses.Add(MapTypingDtoToTypingRelationless(weakness));
            }
            foreach (TypingDto resistance in typingDto.Resistances)
            {
                resistances.Add(MapTypingDtoToTypingRelationless(resistance));
            }
            foreach (TypingDto immunity in typingDto.Immunities)
            {
                immunities.Add(MapTypingDtoToTypingRelationless(immunity));
            }

            return new Typing
            {
                Id = typingDto.Id,
                Name = typingDto.Name,
                Weaknesses = weaknesses,
                Resistances = resistances,
                Immunities = immunities
            };
        }

        private TypingRelationless MapTypingDtoToTypingRelationless(TypingDto typingDto)
        {
            return new TypingRelationless
            {
                Id = typingDto.Id,
                Name = typingDto.Name
            };
        }
    }
}
