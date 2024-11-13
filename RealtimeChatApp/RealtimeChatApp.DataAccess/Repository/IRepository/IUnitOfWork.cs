namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }
        INotificationRepository Notifications { get; }
        IChallangeRepository Challenges { get; }
        IMessageMetadataRepository MessageMetadata { get; }
        IUserStatusHistoryRepository UserStatusHistory { get; }
        IChatParticipantRepository ChatParticipant { get; }
        Task<int> SaveAsync();
    }
}
