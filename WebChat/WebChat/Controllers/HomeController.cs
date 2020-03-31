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

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatService _chatRepository;
        public HomeController(IChatService chatRepository)
        {
            this._chatRepository = chatRepository;
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
            return View();
        }

        [HttpPost]
        public JsonResult GetMessages(MessagesParameters messagesParameters)
        {
            var result = _chatRepository.GetMessages(messagesParameters.StartDate, messagesParameters.EndDate);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}