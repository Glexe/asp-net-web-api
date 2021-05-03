using ef_core_web_api.Models;
using ef_core_web_api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        public abstract TripDTO Get(int id);
        public abstract IEnumerable<TripDTO> GetAll();
        public abstract void AssignClient(int idTrip, int idClient, DateTime? paymentDate);
    }
}
