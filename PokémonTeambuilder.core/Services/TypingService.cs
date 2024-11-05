using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class TypingService
    {
        private readonly ITypingRepos typingRepos;
        public TypingService(ITypingRepos typingRepos)
        {
            this.typingRepos = typingRepos;
        }

        public async Task<List<Typing>> GetAllTypingsAsync()
        {
            List<Typing> typings = [];
            try
            {
                typings = await typingRepos.GetAllTypingsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("could not get Typings");
            }

            foreach (Typing typing in typings)
            {
                ValidateTyping(typing);
            }
            return typings;
        }

        private void ValidateTyping(Typing typing)
        {
            //TODO: make custom Exceptions
            if (typing.Id <= 0)
                throw new Exception("Typing Id cannot be" + typing.Id);
            if (string.IsNullOrEmpty(typing.Name))
                throw new Exception("Typing Name cannot be null or empty");
        }
    }
}
