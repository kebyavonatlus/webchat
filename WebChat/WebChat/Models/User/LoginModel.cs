using System.ComponentModel.DataAnnotations;

namespace WebChat.Models.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Заполните поле \"Логин\" ")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Пароль\" ")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}