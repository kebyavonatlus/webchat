using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using WebChat.Enums;

namespace WebChat.Entity
{
    public class ConnectionInfo
    {
        [Key]
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
        public ConnectionStatus Status { get; set; }
    }
}