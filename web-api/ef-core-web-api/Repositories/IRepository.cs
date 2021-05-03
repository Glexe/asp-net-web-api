using ef_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        abstract Task<T> GetAsync(int id);
        abstract Task<int> AddAsync(T entity);
        abstract Task<T> DeleteAsync(T entity);
        abstract Task<T> UpdateAsync(T entity);

        abstract Task<bool> ContainsAsync(T entity);
    }
}
