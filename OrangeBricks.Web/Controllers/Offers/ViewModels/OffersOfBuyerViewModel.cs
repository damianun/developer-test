using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class OffersOfBuyerViewModel
    {
        public List<OfferOnPropertOfBuyerViewModel> Offers;
        public bool HasOffers { get; set; }
    }

    public class OfferOnPropertOfBuyerViewModel
    {
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public int PropertyId { get; set; }
        public bool Accepted { get; set; }
    }
}