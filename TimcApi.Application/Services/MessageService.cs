using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetConversationAsync(int currentUserId, int otherUserId)
        {
            var result = await _repo.GetMessagesBetweenAsync(currentUserId, otherUserId);
            return _mapper.Map<IEnumerable<MessageDto>>(result);
        }

        public async Task<IEnumerable<MessageDto>> GetInboxAsync(int currentUserId)
        {
            var result = await _repo.GetInboxForUserAsync(currentUserId);
            return _mapper.Map<IEnumerable<MessageDto>>(result);
        }

        public async Task<int> SendAsync(int senderId, SendMessageDto dto)
        {
            var msg = new Message
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };
            return await _repo.SendAsync(msg);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            await _repo.MarkAsReadAsync(messageId);
        }
    }

}
