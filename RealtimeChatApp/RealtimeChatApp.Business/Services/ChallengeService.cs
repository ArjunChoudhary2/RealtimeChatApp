using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallangeRepository _challengeRepository;

        public ChallengeService(IChallangeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Challenges> GetChallengeByIdAsync(int challengeId)
        {
            return await _challengeRepository.GetByIdAsync(challengeId);
        }

        public async Task<IEnumerable<Challenges>> GetAllChallengesAsync()
        {
            return await _challengeRepository.GetAllAsync();
        }

        public async Task CreateChallengeAsync(Challenges challenge)
        {
            await _challengeRepository.AddAsync(challenge);
        }

        public async Task UpdateChallengeAsync(Challenges challenge)
        {
            await _challengeRepository.UpdateAsync(challenge);
        }

        public async Task DeleteChallengeAsync(int challengeId)
        {
            await _challengeRepository.DeleteAsync(challengeId);
        }
    }

}
