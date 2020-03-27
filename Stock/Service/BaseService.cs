using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service
{
    public abstract class BaseService
    {
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DSN1"].ConnectionString;
    }
}
