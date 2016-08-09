using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;

namespace ShipShop.Data.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {
    }

    public class RegionRepository : RepositoryBase<Region>, IRegionRepository
    {
        public RegionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}