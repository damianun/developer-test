using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewinsOnPropertyViewModel
    {
        public string PropertyType { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string StreetName { get; set; }
        public bool HasViewings { get; set; }
        public IEnumerable<ViewingsViewModel> Viewings { get; set; }
        public int PropertyId { get; set; }
    }

    public class ViewingsViewModel
    {
        public int Id { get; set; }
        public DateTime VisitDateTime { get; set; }
        public bool IsPending { get; set; }
        public string Status { get; set; }
    }
}