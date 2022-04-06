using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Squares.API.Domain.Dto
{
    public class SignUpRequestDto
    {
        [DataMember(Name = "firstName")]
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }
    }
}
