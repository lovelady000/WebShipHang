using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;

namespace ShipShop.Data.Repositories
{
    public interface IDonViTieuBieuRepository : IRepository<DonViTieuBieu>
    {
    }

    public class DonViTieuBieuRepository : RepositoryBase<DonViTieuBieu>, IDonViTieuBieuRepository
    {
        public DonViTieuBieuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}