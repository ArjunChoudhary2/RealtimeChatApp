using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using System;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatAppDbContext _context;
        private IUserRepository _users;
        private IChatRepository _chats;
        private IMessageRepository _messages;
        private INotificationRepository _notifications;
        private IChallangeRepository _challenges;
        private IChatParticipantRepository _chatParticipant;
        private IUserStatusHistoryRepository _userStatusHistory;
        private IMessageMetadataRepository _messageMetadata ;

        public UnitOfWork(ChatAppDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IChatRepository Chats => _chats ??= new ChatRepository(_context);
        public IMessageRepository Messages => _messages ??= new MessageRepository(_context);
        public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_context);
        public IChallangeRepository Challenges => _challenges ??= new ChallangeRepository(_context);
        public IMessageMetadataRepository MessageMetadata => _messageMetadata ??= new MessageMetadataRepository(_context);
        public IUserStatusHistoryRepository UserStatusHistory => _userStatusHistory ??= new UserStatusRepository(_context);

        public IChatParticipantRepository ChatParticipant => _chatParticipant  ??=new ChatParticipantRepository(_context);



        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
