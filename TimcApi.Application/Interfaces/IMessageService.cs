using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetConversationAsync(int currentUserId, int otherUserId);
        Task<IEnumerable<MessageDto>> GetInboxAsync(int currentUserId);
        Task<int> SendAsync(int senderId, SendMessageDto dto);
        Task MarkAsReadAsync(int messageId);
    }

}
