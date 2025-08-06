using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesBetweenAsync(int user1Id, int user2Id);
        Task<IEnumerable<Message>> GetInboxForUserAsync(int userId);
        Task<int> SendAsync(Message message);
        Task MarkAsReadAsync(int messageId);
    }

}
