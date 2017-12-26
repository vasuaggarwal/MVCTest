using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        AppDataContext Get();
    }
}
