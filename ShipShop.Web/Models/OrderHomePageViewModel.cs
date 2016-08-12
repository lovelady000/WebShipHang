using System.Collections.Generic;

namespace ShipShop.Web.Models
{
    public class OrderHomePageViewModel
    {
        public OrderViewModel Order { set; get; }

        public List<OrderDetailViewModel> listOrderDetail { set; get; }
    }
}