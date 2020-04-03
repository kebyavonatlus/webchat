using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Messaging;
using WebChat.Entity;
using WebChat.Enums;
using WebChat.Models.Chat;

namespace WebChat.Services.Chat
{
    public interface IChatService
    {
        void AddMessage(MessageLog message);
        void AddInfo(ConnectionInfo info);
        List<MessageModel> GetMessages();
        List<MessageModel> GetMessages(int? UserId, DateTime startDate, DateTime endDate);
    }
}