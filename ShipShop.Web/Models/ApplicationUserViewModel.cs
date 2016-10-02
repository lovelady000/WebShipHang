using System;
using System.Collections.Generic;

namespace ShipShop.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }

        public string UserName { get; set; }

        public string FullName { set; get; }

        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        public bool Vendee { set; get; }

        public string WebOrShopName { set; get; }

        public int RegionID { set; get; }

        public bool IsAdmin { set; get; }

        public string Password { set; get; }

        public bool? IsBanded { set; get; }

        public virtual RegionViewModel Region { set; get; }

        public IEnumerable<ApplicationGroupViewModel> Groups { set; get; }
    }
}