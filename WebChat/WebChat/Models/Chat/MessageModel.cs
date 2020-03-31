using System;

namespace WebChat.Models.Chat
{
    public class MessageModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }
    }
}