namespace ShipShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShipShop.Data.OnlineShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShipShop.Data.OnlineShopDbContext context)
        {
            CreateAccount(context);
            CreateRegionAndAreas(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void CreateAccount(ShipShop.Data.OnlineShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "onlineshop",
                Email = "nguyendunghn0109@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nguyễn Năng Dũng",
                PhoneNumber = "0983007974",
                PhoneNumberConfirmed = true,
            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nguyendunghn0109@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateRegionAndAreas(OnlineShopDbContext context)
        {
            if (context.Areas.Count() == 0)
            {
                var area1 = new Areas()
                {
                    Name = "Vùng 1",
                };
                context.Areas.Add(area1);
                context.SaveChanges();
                List<Region> listRegion = new List<Region>()
                {
                    new Region() {Name="Hoàn Kiếm",AreaID=area1.AreaID },
                    new Region() {Name="Tây Hồ",AreaID=area1.AreaID },
                    new Region() {Name="Ba Đình",AreaID=area1.AreaID },
                    new Region() {Name="Cầu giấy",AreaID=area1.AreaID },
                    new Region() {Name="Hai Bà Trưng",AreaID=area1.AreaID },
                    new Region() {Name="Thanh Xuân",AreaID=area1.AreaID },
                    new Region() {Name="Hoàng Mai",AreaID=area1.AreaID },
                };
                context.Regions.AddRange(listRegion);
                context.SaveChanges();
                var area2 = new Areas()
                {
                    Name = "Vùng 2",
                };
                context.Areas.Add(area2);
                context.SaveChanges();
                List<Region> listRegion2 = new List<Region>()
                {
                    new Region() {Name="Long Biên",AreaID=area2.AreaID },
                    new Region() {Name="Bắc Từ Liêm",AreaID=area2.AreaID },
                    new Region() {Name="Nam Từ Liêm",AreaID=area2.AreaID },
                    new Region() {Name="Hà Đông",AreaID=area2.AreaID },
                    new Region() {Name="Thanh Trì",AreaID=area2.AreaID },
                    new Region() {Name="Gia Lâm",AreaID=area2.AreaID },
                    new Region() {Name="Đông Anh",AreaID=area2.AreaID },
                };

                context.Regions.AddRange(listRegion2);
                context.SaveChanges();
            }
        }
    }
}