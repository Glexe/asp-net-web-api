using ef_core_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TripContext _dbContext;

        public ClientRepository(TripContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetAsync(int id)
        {
            return await _dbContext.Set<Client>().Include(t => t.ClientTrips).FirstAsync(e => e.IdClient == id);
        }
        public async Task<int> AddAsync(Client entity)
        {
            var clients = _dbContext.Set<Client>();
            entity.IdClient = clients.Any() ? clients.Max(e => e.IdClient) + 1 : 1;

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.IdClient;
        }
        public async Task<Client> DeleteAsync(Client entity)
        {
            if (entity.ClientTrips != null && entity.ClientTrips.Count > 0)
            {
                return null;
            }

            var result = _dbContext.Set<Client>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public IEnumerable<Client> Get(Func<Client, bool> pred)
        {
            return _dbContext.Set<Client>().Where(pred);
        }
        public async Task<bool> ContainsAsync(Client entity)
        {
            return await _dbContext.Set<Client>().AnyAsync(e => e.Pesel == entity.Pesel);
        }

        #region NotImplemented
        public Task<Client> UpdateAsync(Client entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
