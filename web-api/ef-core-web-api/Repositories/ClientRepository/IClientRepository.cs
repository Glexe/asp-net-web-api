using ef_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        public abstract IEnumerable<Client> Get(Func<Client, bool> pred);
    }
}
