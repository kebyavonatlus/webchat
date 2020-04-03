using System;

namespace WebChat.ResourceParameters
{
    public class MessagesParameters
    {
        public int? UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}