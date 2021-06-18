using System.ComponentModel.DataAnnotations;

namespace Microservice.Business.Models
{
    public class RegisterModel
    {
         public string Username { get; set; }
 
         [Required(ErrorMessage = Constants.EmptyUsernameOrPassword)]
         public string Password { get; set; }
         
         [Required(ErrorMessage = Constants.EmptyUserIdentifier)]
         public string Identifier { get; set; }
 
         [Required(ErrorMessage = Constants.EmptyUsernameOrPassword)]
         [EmailAddress]
         public string EmailAddress { get; set; }
 
         public string FirstName { get; set; }
         public string LastName { get; set; }
    }
}