using System;
using System.ComponentModel.DataAnnotations;

namespace WebChat.Entity
{
    public class MessageLog
    {
        [Key]
        public int MessageId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }
    }
}