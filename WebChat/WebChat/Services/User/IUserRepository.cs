using WebChat.Models.User;

namespace WebChat.Services.User
{
    public interface IUserRepository
    {
        void UserRegister(RegisterModel register);
        Entity.User UserIsExist(string userName);
        Entity.User UserLogin(Entity.User user);

    }
}