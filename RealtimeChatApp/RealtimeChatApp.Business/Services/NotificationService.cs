using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notifications> GetNotificationByIdAsync(int notificationId)
        {
            return await _notificationRepository.GetByIdAsync(notificationId);
        }

        public async Task<IEnumerable<Notifications>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task AddNotificationAsync(Notifications notification)
        {
            await _notificationRepository.AddAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notifications notification)
        {
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            await _notificationRepository.DeleteAsync(notificationId);
        }
    }

}
