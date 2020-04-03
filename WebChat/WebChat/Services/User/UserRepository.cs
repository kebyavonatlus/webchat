using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebChat.DbContext;
using WebChat.Entity;
using WebChat.Models.User;

namespace WebChat.Services.User
{
    public class UserRepository : IUserRepository
    {
        public void UserRegister(RegisterModel register)
        {
            using (var context = new ChatContext())
            {
                using (var transactions = context.Database.BeginTransaction())
                {
                    var newUser = new Entity.User
                    {
                        UserName = register.UserName,
                        Password = GetHashCodeMd5(register.Password),
                        FullName = register.FullName
                    };
                    context.Users.Add(newUser);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Не удалось сохранить пользователя, попробуйте позже.");
                    }
                    transactions.Commit();
                }
            }
        }

        public Entity.User UserIsExist(string userName)
        {
            using (var context = new ChatContext())
            {
                return context.Users.FirstOrDefault(u => u.UserName == userName);
            }
        }

        public Entity.User UserLogin(Entity.User user)
        {
            using (var context = new ChatContext())
            {
                var password = GetHashCodeMd5(user.Password);
                return context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == password);
            }
        }

        public List<Entity.User> GetAllUsers()
        {
            using (var db = new ChatContext())
            {
                return db.Users.ToList();
            }
        }

        private string GetHashCodeMd5(string password)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}