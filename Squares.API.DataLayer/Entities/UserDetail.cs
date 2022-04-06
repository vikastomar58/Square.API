using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Squares.API.DataLayer.Entities
{
    [Table("tbl_UserDetails")]
   public class UserDetail
    {
        [Column("User_Id")]
        public int Id { get; set; }

        [Required]
        [Column("First_Name", TypeName ="nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("Last_Name",TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Required]
        [Column("Email", TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column("Password", TypeName = "nvarchar(500)")]
        public string Password { get; set; }

        [Column("Role", TypeName = "nvarchar(20)")]
        public string Role { get; set; }

        [Column("Salt", TypeName = "nvarchar(100)")]
        public string Salt { get; set; }
    }
}
