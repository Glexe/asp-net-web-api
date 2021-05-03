using ef_core_web_api.Models;
using ef_core_web_api.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _dbContext;

        public TripRepository(TripContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TripDTO Get(int id)
        {
            var trip = _dbContext.Trips.FirstOrDefault(trip => trip.IdTrip == id);

            if (trip is null) return null;

            var countries = _dbContext.Set<Country>();
            var countryTrips = _dbContext.Set<CountryTrip>();
            var clients = _dbContext.Set<Client>();
            var clientTrips = _dbContext.Set<ClientTrip>();

            var countriesDto = from country in countries
                               join countryTrip in countryTrips on country.IdCountry equals countryTrip.IdCountry
                               where countryTrip.IdTrip == trip.IdTrip
                               select new CountryDTO(country.Name);

            var clientsDto = from client in clients
                             join clientTrip in clientTrips on client.IdClient equals clientTrip.IdClient
                             where clientTrip.IdTrip == trip.IdTrip
                             select new ClientDTO(client.FirstName, client.LastName);

            return new TripDTO(trip.Name, trip.Description, trip.DateFrom, trip.DateTo, trip.MaxPeople, countriesDto, clientsDto);
        }

        public IEnumerable<TripDTO> GetAll()
        {
            var trips = _dbContext.Set<Trip>();

            IEnumerable<TripDTO> getTripsFullInfo()
            {
                foreach (var trip in trips)
                {
                    yield return Get(trip.IdTrip);
                }
            }

           return getTripsFullInfo();
        }

        public async Task<bool> ContainsAsync(Trip trip)
        {
            return await _dbContext.Set<Trip>().AnyAsync(t => t.IdTrip == trip.IdTrip && t.Name == trip.Name);
        }

        public async void AssignClient(int idTrip, int idClient, DateTime? paymentDate)
        {
            var clientTrip = new ClientTrip()
            {
                IdClient = idClient,
                IdTrip = idTrip,
                PaymentDate = paymentDate,
                RegisteredAt = DateTime.Now
            };

            await _dbContext.Set<ClientTrip>().AddAsync(clientTrip);
            _dbContext.SaveChanges();
        }
        public async Task<Trip> GetAsync(int id)
        {
            return await _dbContext.Set<Trip>().Include(t => t.ClientTrips).Include(c => c.CountryTrips).FirstAsync(t => t.IdTrip == id);
        }

        #region NotImplemented

        public Task<int> AddAsync(Trip entity)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> DeleteAsync(Trip entity)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> UpdateAsync(Trip entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
