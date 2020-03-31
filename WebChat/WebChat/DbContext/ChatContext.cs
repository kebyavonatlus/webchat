using System.Data.Entity;
using WebChat.Entity;

namespace WebChat.DbContext
{
    public class ChatContext : System.Data.Entity.DbContext
    {
        public ChatContext () : base("DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<ConnectionInfo> ConnectionInfoes { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }
    }
}