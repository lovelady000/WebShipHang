using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;

namespace ShipShop.Data.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
    }

    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}