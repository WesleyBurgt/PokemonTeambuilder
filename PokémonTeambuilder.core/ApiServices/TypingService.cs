using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
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

        public async Task GetAllTypingsAndSaveThem()
        {
            List<Typing> typings = await typingWrapper.GetAllTypings();
            foreach (Typing typing in typings)
            {
                ValidateTyping(typing);
            }
            await typingRepos.SetAllTypingsAsync(typings);
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
