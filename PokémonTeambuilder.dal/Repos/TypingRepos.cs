using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PokémonTeambuilder.core.DbInterfaces;
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
            List<Typing> typings = await context.Typings.ToListAsync();
            return typings;
        }

        public async Task SetAllTypingsAsync(List<Typing> typings)
        {
            Dictionary<int, Typing> existingTypings = await context.Typings.ToDictionaryAsync(t => t.Id);

            List<Typing> typingsList = new List<Typing>();
            foreach (Typing typing in typings)
            {
                Typing typingToAdd;

                if (existingTypings.TryGetValue(typing.Id, out Typing? existingTyping))
                {
                    typingToAdd = existingTyping;
                }
                else
                {
                    typingToAdd = new Typing { Id = typing.Id, Name = typing.Name, Relationships = new List<TypingRelationship>() };
                    context.Typings.Add(typingToAdd);
                }

                typingsList.Add(typingToAdd);
            }
            await context.SaveChangesAsync();

            foreach (EntityEntry<Typing>? entry in context.ChangeTracker.Entries<Typing>())
            {
                entry.State = EntityState.Detached;
            }

            foreach (Typing typing in typings)
            {
                Typing? mainTyping = typingsList.FirstOrDefault(t => t.Id == typing.Id);

                if (mainTyping != null)
                {
                    if (mainTyping.Relationships == null)
                    {
                        mainTyping.Relationships = new List<TypingRelationship>();
                    }

                    foreach (TypingRelationship typingRelationship in typing.Relationships)
                    {
                        if (existingTypings.TryGetValue(typingRelationship.TypingId, out Typing? existingTyping) &&
                            existingTypings.TryGetValue(typingRelationship.RelatedTypingId, out Typing? relatedTyping))
                        {
                            mainTyping.Relationships.Add(new TypingRelationship
                            {
                                TypingId = typingRelationship.TypingId,
                                Typing = existingTyping,
                                RelatedTypingId = typingRelationship.RelatedTypingId,
                                RelatedTyping = relatedTyping,
                                Relation = typingRelationship.Relation
                            });
                        }
                        else
                        {
                            throw new Exception("Could not find typing relationships.");
                        }
                    }
                    context.Typings.Update(mainTyping);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
