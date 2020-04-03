using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Microsoft.AspNet.SignalR.Messaging;
using WebChat.DbContext;
using WebChat.Entity;
using WebChat.Enums;
using WebChat.Models.Chat;

namespace WebChat.Services.Chat
{
    public class ChatService : IChatService
    {
        public void AddInfo(ConnectionInfo info)
        {
            using (var db = new ChatContext())
            {
                db.ConnectionInfoes.Add(info);
                db.SaveChanges();
            }
        }

        public List<MessageModel> GetMessages()
        {
            using (var db = new ChatContext())
            {
                var result = from message in db.MessageLogs
                             join user in db.Users on message.UserId equals user.UserId
                             where DbFunctions.TruncateTime(message.MessageDate) >= DbFunctions.TruncateTime(DateTime.Now)
                             select new MessageModel
                             {
                                 UserName = user.FullName,
                                 UserId = user.UserId,
                                 MessageDate = message.MessageDate,
                                 Content = message.Content
                             };
                return result.ToList();
            }
        }

        public List<MessageModel> GetMessages(int? userId, DateTime startDate, DateTime endDate)
        {
            using (var db = new ChatContext())
            {
                if (userId != null)
                {
                    var userResult = from message in db.MessageLogs
                        join user in db.Users on message.UserId equals user.UserId
                        where (DbFunctions.TruncateTime(message.MessageDate) >= startDate &&
                               DbFunctions.TruncateTime(message.MessageDate) <= endDate) && user.UserId == userId
                        select new MessageModel
                        {
                            UserName = user.FullName,
                            UserId = user.UserId,
                            MessageDate = message.MessageDate,
                            Content = message.Content
                        };
                    return userResult.ToList();
                }
                var result = from message in db.MessageLogs
                    join user in db.Users on message.UserId equals user.UserId
                    where (DbFunctions.TruncateTime(message.MessageDate) >= startDate &&
                           DbFunctions.TruncateTime(message.MessageDate) <= endDate)
                    select new MessageModel
                    {
                        UserName = user.FullName,
                        UserId = user.UserId,
                        MessageDate = message.MessageDate,
                        Content = message.Content
                    };
                return result.ToList();
            }
        }

        public void AddMessage(MessageLog message)
        {
            using (var db = new ChatContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.MessageLogs.Add(message);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Что-то пошло не так. Попробуйте снова.");
                    }
                    transaction.Commit();
                }
            }
        }
    }
}