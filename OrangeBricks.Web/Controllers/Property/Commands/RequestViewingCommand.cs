using System;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class RequestViewingCommand
    {
        public DateTime VisitDate { get; set; }

        public string VisitorUserId { get; set; }
    }
}