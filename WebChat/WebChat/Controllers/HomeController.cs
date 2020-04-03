using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using WebChat.Entity;
using WebChat.ResourceParameters;
using WebChat.Services.Chat;
using WebChat.Services.User;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatService _chatRepository;
        private readonly IUserRepository _userRepository;
        public HomeController(IChatService chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }
        [Authorize]
        public ActionResult Chat()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChatHistory()
        {
            ViewBag.Users = _userRepository.GetAllUsers();
            return View();
        }

        [HttpPost]
        public JsonResult GetMessages(MessagesParameters messagesParameters)
        {
            var result = _chatRepository.GetMessages(messagesParameters.UserId, messagesParameters.StartDate, messagesParameters.EndDate);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}