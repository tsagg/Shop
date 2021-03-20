using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Заполните поле \"Имя\"")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Заполните поле \"Фамилия\"")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Заполните поле \"Отчество\"")]
        public string MiddleName { get; set; }
    }

    public class EditModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}