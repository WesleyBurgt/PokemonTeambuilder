using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Typing>> GetAllTypings()
        {
            List<Typing> typings = await context.Typings.ToListAsync();
            return typings;
        }

        public void SetAllTypings(List<Typing> typings)
        {
            // Cache all existing typings in memory
            var existingTypings = context.Typings.ToDictionary(t => t.Id);

            // Create new typings without duplicating entities
            var typingsList = new List<Typing>();
            foreach (var typing in typings)
            {
                Typing typingToAdd;

                if (existingTypings.TryGetValue(typing.Id, out var existingTyping))
                {
                    typingToAdd = existingTyping; // Use the existing instance
                }
                else
                {
                    typingToAdd = new Typing { Id = typing.Id, Name = typing.Name, Relationships = new List<TypingRelationship>() };
                    context.Typings.Add(typingToAdd); // Add new instance to context
                }

                typingsList.Add(typingToAdd);
            }
            context.SaveChanges();

            // Detach all tracked Typing entities before updating relationships to avoid duplicates
            foreach (var entry in context.ChangeTracker.Entries<Typing>())
            {
                entry.State = EntityState.Detached;
            }

            // Reattach typings to set relationships
            foreach (var typing in typings)
            {
                var mainTyping = typingsList.FirstOrDefault(t => t.Id == typing.Id);

                // Ensure that Relationships is initialized
                if (mainTyping.Relationships == null)
                {
                    mainTyping.Relationships = new List<TypingRelationship>();
                }

                foreach (var typingRelationship in typing.Relationships)
                {
                    if (existingTypings.TryGetValue(typingRelationship.TypingId, out var existingTyping) &&
                        existingTypings.TryGetValue(typingRelationship.RelatedTypingId, out var relatedTyping))
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
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex) { }
        }
    }
}
