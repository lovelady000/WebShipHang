using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Models
{
    public class MenuGroupViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public virtual IEnumerable<MenuViewModel> Menus { set; get; }
    }
}