using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Squares.API.DataLayer.Entities
{
    [Table("tbl_Points")]
    public  class Coordinate
    {
        [Column("Point_Id")]
        public int Id { get; set; }

        [Column("X")]
        [Required]
        public int X { get; set; }

        [Column("Y")]
        [Required]
        public int Y { get; set; }

        [ForeignKey("UserDetail")]
        [Required]
        [Column("User_Id")]
        public virtual int UserId { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}
