using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public interface INotificationService
    {
        Task<Notifications> GetNotificationByIdAsync(int notificationId);
        Task<IEnumerable<Notifications>> GetAllNotificationsAsync();
        Task AddNotificationAsync(Notifications notification);
        Task UpdateNotificationAsync(Notifications notification);
        Task DeleteNotificationAsync(int notificationId);
    }

}
