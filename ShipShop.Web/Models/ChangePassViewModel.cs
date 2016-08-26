using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Models
{
    public class ChangePassViewModel
    {
        public string Id { set; get; }

        public string UserName { set; get; }

        public string OldPassword { set; get; }

        public string NewPassword { set; get; }

        public string RePassword { set; get; }
    }
}