using System.Collections.Generic;

namespace ShipShop.Web.Models
{
    public class AreaViewModel
    {
        public int AreaID { set; get; }

        public string Name { set; get; }

        public IEnumerable<RegionViewModel> Region { set; get; }
    }
}