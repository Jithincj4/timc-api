using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public MessageRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenAsync(int user1Id, int user2Id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<Message>(@"
            SELECT * FROM Messages
            WHERE (SenderId = @user1Id AND ReceiverId = @user2Id)
               OR (SenderId = @user2Id AND ReceiverId = @user1Id)
            ORDER BY SentAt",
                new { user1Id, user2Id });
        }

        public async Task<IEnumerable<Message>> GetInboxForUserAsync(int userId)
        {
            var conn = _connFactory.CreateConnection();

            return await conn.QueryAsync<Message>(@"
            SELECT * FROM Messages
            WHERE ReceiverId = @userId OR SenderId = @userId
            ORDER BY SentAt DESC", new { userId });
        }

        public async Task<int> SendAsync(Message message)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            INSERT INTO Messages (SenderId, ReceiverId, Content, SentAt, IsRead)
            VALUES (@SenderId, @ReceiverId, @Content, @SentAt, 0);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, message);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var conn = _connFactory.CreateConnection();
            await conn.ExecuteAsync(
                "UPDATE Messages SET IsRead = 1 WHERE MessageId = @messageId",
                new { messageId });
        }
    }

}
