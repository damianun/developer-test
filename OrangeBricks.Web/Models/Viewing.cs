using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime VisitAt { get; set; }

        [Column("Property_Id")]
        public Property Property { get; set; }

        [Required]
        public ViewingStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public string VisitorUserId { get; set; }
    }
}