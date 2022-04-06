using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Squares.API.Domain.Dto
{
    public class LoginRequestDto
    {
        [DataMember(Name = "email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
