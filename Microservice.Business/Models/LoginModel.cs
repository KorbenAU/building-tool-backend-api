using System.ComponentModel.DataAnnotations;

namespace Microservice.Business.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = Constants.EmptyUsernameOrPassword)]
        public string Username { get; set; }

        [Required(ErrorMessage = Constants.EmptyUsernameOrPassword)]
        public string Password { get; set; }
    }
}