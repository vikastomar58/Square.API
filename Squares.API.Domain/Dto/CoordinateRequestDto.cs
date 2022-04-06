using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Squares.API.Domain.Dto
{
    public class CoordinateRequestDto
    {
        [DataMember(Name = "x")]
        [Required(ErrorMessage = "Cordinate X is required")]
        public int X { get; set; }

        [DataMember(Name = "y")]
        [Required(ErrorMessage = "Cordinate X is required")]
        public int Y { get; set; }
    }
}
