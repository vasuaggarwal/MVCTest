using MVC.Data.Infrastructure;
using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Data.Repositories
{
    public class CostRepository : RepositoryBase<Cost>, ICostRepository
    {
        public CostRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }
    }
    public interface ICostRepository:IRepository<Cost>
    {

    }
}
