using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Поле электронной почты должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Некорректная электронная почта")]
        [Display(Name = "Электронная почта")]
        public string email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле пароль должно быть заполнено")]
        public string password { get; set; }
    }

    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Поле электронной почты должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Некорректная электронная почта")]
        [Display(Name = "Электронная почта")]
        public string email { get; set; }

        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        [RegularExpression(@"^[a-zA-Z''-'\s0-9]{1,40}$", ErrorMessage = "Пароль может содержать символы латинского алфавита и цифры")]
        [Required(ErrorMessage = "Поле пароль должно быть заполнено")]
        public string password { get; set; }

        [Display(Name = "Подтвеждение пароля")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("password", ErrorMessage ="Пароли не совпадают")]
        public string password2 { get; set; }

        [Display(Name = "Номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string phoneNumber { get; set; }

        [Display(Name = "Ваше имя")]
        public string name { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Поле электронной почты должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Некорректная электронная почта")]
        [Display(Name = "Электронная почта")]
        public string email { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        [RegularExpression(@"^[a-zA-Z''-'\s0-9]{1,40}$", ErrorMessage = "Пароль может содержать символы латинского алфавита и цифры")]
        [Required(ErrorMessage = "Поле пароль должно быть заполнено")]
        public string password { get; set; }

        [Display(Name = "Подтвеждение пароля")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        public string password2 { get; set; }

        [Required]
        public Guid token { get; set; }
    }

}