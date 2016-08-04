using System;

namespace ShipShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        OnlineShopDbContext Init();
    }
}