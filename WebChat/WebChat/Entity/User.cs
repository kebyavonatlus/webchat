using System.ComponentModel.DataAnnotations;

namespace WebChat.Entity
{
    public class User
    {
        [Key]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Логин\" ")]
        [MaxLength(50)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Заполните поле \"ФИО\" ")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}