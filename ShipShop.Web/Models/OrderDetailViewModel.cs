namespace ShipShop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { set; get; }

        public int OrderID { set; get; }

        public string NameProduct { set; get; }

        public string UrlProductDetail { set; get; }

        public string Note { set; get; }

        public virtual OrderViewModel Order { set; get; }
    }
}