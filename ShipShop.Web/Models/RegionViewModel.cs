using ShipShop.Model.Models;

namespace ShipShop.Web.Models
{
    public class RegionViewModel
    {
        public int RegionID { set; get; }

        public int AreaID { set; get; }

        public string Name { set; get; }

        public virtual Area Areas { set; get; }
    }
}