using ShipShop.Data.Infrastructure;
using ShipShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Data.Repositories
{
    public interface INewsRepository : IRepository<News>
    {

    }
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
