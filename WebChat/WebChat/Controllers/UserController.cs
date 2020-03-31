using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;
using WebChat.Entity;
using WebChat.Models.User;
using WebChat.Services.Chat;
using WebChat.Services.User;
using WebChat.SignalR;

namespace WebChat.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IChatService _chatRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository repository, IChatService chatRepository, IUserRepository userRepository)
        {
            this._repository = repository;
            this._chatRepository = chatRepository;
            this._userRepository = userRepository;
        }
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.UserIsExist(register.UserName);
                if (user == null)
                {
                    _repository.UserRegister(register);
                    user = _repository.UserIsExist(register.UserName);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(register.UserName, true);
                        return RedirectToAction("Chat", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                }
            }
            return View(register);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var checkUser = new User
                {
                    UserName = login.UserName,
                    Password = login.Password
                };

                var returnUser = _repository.UserLogin(checkUser);
                if (returnUser != null)
                {
                    FormsAuthentication.SetAuthCookie(returnUser.UserName, true);
                    return RedirectToAction("Chat", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
            }

            return View(login);
        }

        public ActionResult GetMessages()
        {
            var result = _chatRepository.GetMessages();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}