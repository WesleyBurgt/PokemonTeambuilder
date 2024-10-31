using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class TypingService
    {
        private readonly ITypingWrapper typingWrapper;
        public TypingService(ITypingWrapper typingWrapper)
        {
            this.typingWrapper = typingWrapper;
        }

        public async Task<List<Typing>> GetAllTypingsFromApi()
        {
            List<Typing> list = await typingWrapper.GetAllTypings();
            foreach (Typing typing in list)
            {
                //TODO: make custom Exceptions
                if (typing.Id <= 0)
                    throw new Exception("Typing Id cannot be" + typing.Id);
                if (string.IsNullOrEmpty(typing.Name))
                    throw new Exception("Typing Name cannot be null or empty");
            }
            return list;
        }
    }
}
