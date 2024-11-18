using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
namespace PokémonTeambuilder.core.ApiServices
{
    public class TypingService
    {
        private readonly ITypingWrapper typingWrapper;
        private readonly ITypingRepos typingRepos;

        public TypingService(ITypingWrapper typingWrapper, ITypingRepos typingRepos)
        {
            this.typingWrapper = typingWrapper;
            this.typingRepos = typingRepos;
        }

        public async Task FetchAndSaveTypingsAsync()
        {
            List<Typing> typings = await typingWrapper.GetAllTypingsAsync();
            foreach (Typing typing in typings)
            {
                ValidateTyping(typing);
            }
            await typingRepos.SetAllTypingsAsync(typings);
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
