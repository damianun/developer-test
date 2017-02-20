using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewingsOfBuyerViewModel
    {
        public List<ViewingOnPropertOfBuyerViewModel> Viewings;
        public bool HasViewings { get; set; }
    }

    public class ViewingOnPropertOfBuyerViewModel
    {
        public DateTime VisitDateTime { get; set; }
        public string Status { get; set; }
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public int PropertyId { get; set; }
        public bool Accepted { get; set; }
    }
}