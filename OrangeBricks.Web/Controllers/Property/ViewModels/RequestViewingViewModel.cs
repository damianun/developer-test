using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class RequestViewingViewModel
    {
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        [Required]
        [Display(Name = "Viewing Time")]
        [DataType(DataType.DateTime)]
        public DateTime VisitDateTime { get; set; }
        public int PropertyId { get; set; }
    }
}