using ShipShop.Model.Models;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Infrastructure.Extensions
{
    public static class EnityExtensions
    {
        public static void UpdateMenu(this Menu menu, MenuViewModel menuVM) {
            menu.ID  =menuVM.ID;
            menu.Name = menuVM.Name;
            menu.URL = menuVM.URL;
            menu.DisplayOrder = menuVM.DisplayOrder;
            menu.GroupID = menuVM.GroupID;
            menu.Target = menuVM.Target;
            menu.Status = menuVM.Status;
        }

    }
}