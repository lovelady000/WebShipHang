using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShipShop.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { set; get; }

        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        public bool Vendee { set; get; }

        public string WebOrShopName { set; get; }

        public int RegionID { set; get; }

        public bool IsAdmin { set; get; }
        //public virtual IEnumerable<Order> Order { set; get; }

        [ForeignKey("RegionID")]
        public virtual Region Region { set; get; }

        public bool? IsBanded { set; get; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}