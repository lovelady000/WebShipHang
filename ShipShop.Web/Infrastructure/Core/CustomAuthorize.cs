using System;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Infrastructure.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        public string UserRole { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            string CurrentUserRole = "Admin";
            if (this.UserRole.Contains(CurrentUserRole))
            {
                return true;
            }
            else
                return false;
        }
    }
}