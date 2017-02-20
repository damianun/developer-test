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
    }
}