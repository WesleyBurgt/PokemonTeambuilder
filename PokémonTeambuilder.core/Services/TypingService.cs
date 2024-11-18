using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
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
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Typings", ex);
            }

            foreach (Typing typing in typings)
            {
                ValidateTyping(typing);
            }
            return typings;
        }

        public async Task<int> GetTypingCountAsync()
        {
            try
            {
                int result = await typingRepos.GetTypingCountAsync();
                return result;
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Typing count", ex);
            }
        }

        private void ValidateTyping(Typing typing)
        {
            if (typing.Id <= 0)
                throw new InvalidIdException("Typing Id cannot be" + typing.Id, typing.Id, typing.GetType());
            if (string.IsNullOrEmpty(typing.Name))
                throw new InvalidNameException("Typing Name cannot be null or empty", typing.Name, typing.GetType());
        }
    }
}
