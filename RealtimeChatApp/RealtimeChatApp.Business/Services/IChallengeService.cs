using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public interface IChallengeService
    {
        Task<Challenges> GetChallengeByIdAsync(int challengeId);
        Task<IEnumerable<Challenges>> GetAllChallengesAsync();
        Task CreateChallengeAsync(Challenges challenge);
        Task UpdateChallengeAsync(Challenges challenge);
        Task DeleteChallengeAsync(int challengeId);
    }

}
