using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;
using Unity;
using WebChat.Services.Chat;
using WebChat.Services.User;
using WebChat.SignalR;

[assembly: OwinStartup(typeof(WebChat.Startup))]
namespace WebChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(ChatHub),
                () => new ChatHub(new UserRepository(), new ChatService()));
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}