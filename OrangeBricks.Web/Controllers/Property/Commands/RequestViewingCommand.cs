using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class RequestViewingCommand
    {
        public int PropertyId { get; set; }

        [Required]        
        public DateTime VisitDateTime { get; set; }

        public string VisitorUserId { get; set; }
    }
}