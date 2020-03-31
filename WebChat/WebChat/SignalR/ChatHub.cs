using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using WebChat.DbContext;
using WebChat.Entity;
using WebChat.Enums;
using WebChat.Services.Chat;
using WebChat.Services.User;

namespace WebChat.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatService _chatRepository;
        private static List<ConnectionInfo> _users = new List<ConnectionInfo>();

        public ChatHub(IUserRepository _userRepository, IChatService _chatRepository)
        {
            this._userRepository = _userRepository;
            this._chatRepository = _chatRepository;
        }

        public void LetsChat(string name, string message)
        {
            var user = _userRepository.UserIsExist(name);
            if (user != null)
            {
                var newMessage = new Entity.MessageLog
                {
                    UserId = user.UserId,
                    Content = message.Trim(),
                    MessageDate = DateTime.Now
                };
                Clients.All.SendAsync(user.FullName, message, newMessage.MessageDate);
                if (newMessage.Content.Trim() == "")
                {
                    return;
                }
                _chatRepository.AddMessage(newMessage);
            }
        }

        public void Connect(string name)
        {
            var clientId = Context.ConnectionId;
            var u = _userRepository.UserIsExist(name);
            if (!_users.Exists(x => x.UserId == u.UserId))
            {
                var newOnlineUser = new ConnectionInfo
                {
                    UserId = u.UserId,
                    UserName = u.FullName,
                    ConnectionId = clientId,
                    Date = DateTime.Now,
                    Status = ConnectionStatus.Connect
                };
                _users.Add(newOnlineUser);
                _chatRepository.AddInfo(newOnlineUser);
                // Send the current users
                Clients.Caller.onConnected(clientId, u.FullName, _users);
                Clients.AllExcept(clientId).onNewUserConnected(clientId, u.FullName);
            }
            else
            {
                Clients.Caller.onConnected(clientId, u.FullName, _users);
            }
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                var user = _userRepository.UserIsExist(Context.User.Identity.Name);
                var deleteUser = _users.FirstOrDefault(x => x.UserId == user.UserId);
                if (deleteUser != null)
                {
                    _users.Remove(deleteUser);
                    Clients.All.onUserDisconnected(deleteUser.UserId, deleteUser.UserName);
                    var disconnectUser = new ConnectionInfo
                    {
                        UserId = deleteUser.UserId,
                        UserName = deleteUser.UserName,
                        ConnectionId = deleteUser.ConnectionId,
                        Date = DateTime.Now,
                        Status = ConnectionStatus.Disconnect
                    };
                    _chatRepository.AddInfo(disconnectUser);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}