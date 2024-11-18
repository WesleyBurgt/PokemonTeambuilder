using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class TypingRepos : ITypingRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public TypingRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Typing>> GetAllTypingsAsync()
        {
            List<Typing> typings = await context.Typings
                .Include(t => t.Relationships)
                    .ThenInclude(tr => tr.RelatedTyping)
                .ToListAsync();
            return typings;
        }

        public async Task<int> GetTypingCountAsync()
        {
            int count = await context.Typings.CountAsync();
            return count;
        }

        public async Task SetAllTypingsAsync(List<Typing> typings)
        {
            Dictionary<int, Typing> existingTypings = await context.Typings
                .Include(t => t.Relationships)
                .ThenInclude(tr => tr.RelatedTyping)
                .ToDictionaryAsync(t => t.Id);

            foreach (Typing typing in typings)
            {
                if (existingTypings.TryGetValue(typing.Id, out Typing? existingTyping))
                {
                    existingTyping.Name = typing.Name;
                }
                else
                {
                    Typing newTyping = new Typing
                    {
                        Id = typing.Id,
                        Name = typing.Name,
                        Relationships = new List<TypingRelationship>()
                    };
                    context.Typings.Add(newTyping);
                    existingTypings[typing.Id] = newTyping;
                }
            }
            await context.SaveChangesAsync();

            foreach (Typing typing in typings)
            {
                if (existingTypings.TryGetValue(typing.Id, out Typing? mainTyping))
                {
                    mainTyping.Relationships.Clear();

                    foreach (TypingRelationship typingRelationship in typing.Relationships)
                    {
                        if (existingTypings.TryGetValue(typingRelationship.RelatedTypingId, out Typing? relatedTyping))
                        {
                            mainTyping.Relationships.Add(new TypingRelationship
                            {
                                TypingId = mainTyping.Id,
                                Typing = mainTyping,
                                RelatedTypingId = relatedTyping.Id,
                                RelatedTyping = relatedTyping,
                                Relation = typingRelationship.Relation
                            });
                        }
                        else
                        {
                            throw new ReposResponseException("Related typing not found.");
                        }
                    }
                    context.Typings.Update(mainTyping);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
