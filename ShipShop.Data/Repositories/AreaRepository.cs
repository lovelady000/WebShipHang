using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;

namespace ShipShop.Data.Repositories
{
    public interface IAreaRepository : IRepository<Areas>
    {
    }

    public class AreaRepository : RepositoryBase<Areas>, IAreaRepository
    {
        public AreaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}