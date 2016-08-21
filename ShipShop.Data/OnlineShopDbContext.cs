using Microsoft.AspNet.Identity.EntityFramework;
using ShipShop.Model.Models;
using System.Data.Entity;

namespace ShipShop.Data
{
    public class OnlineShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineShopDbContext() : base("OnlineShopConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }

        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }

        public DbSet<News> News { set; get; }
        public DbSet<DonViTieuBieu> DonViTieuBieu { set; get; }
        public DbSet<Region> Regions { set; get; }
        public DbSet<Area> Areas { set; get; }
        public DbSet<WebInformation> WebInformations { set; get; }



        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }

        public DbSet<ApplicationRole> ApplicationRoles { set; get; }

        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }

        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }


        public static OnlineShopDbContext Create()
        {
            return new OnlineShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(x => new { x.UserId, x.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(x => x.UserId).ToTable("ApplicationUserLogins");

            builder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
    }
}