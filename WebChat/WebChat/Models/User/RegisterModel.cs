using System.ComponentModel.DataAnnotations;

namespace WebChat.Models.User
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Заполните поле \"Логин\" ")]
        [MaxLength(50)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните поле \"ФИО\" ")]
        [MaxLength(100)]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Пароль\" ")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Повторите пароль\" ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }

    }
}