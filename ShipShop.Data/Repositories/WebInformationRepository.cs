using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;

namespace ShipShop.Data.Repositories
{
    public interface IWebInformationRepository : IRepository<WebInformation>
    {
    }

    public class WebInformationRepository : RepositoryBase<WebInformation>, IWebInformationRepository
    {
        public WebInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}