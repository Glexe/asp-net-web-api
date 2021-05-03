using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ef_core_web_api.Models;
using ef_core_web_api.Repositories;
using ef_core_web_api.Models.DTO;
using System;

namespace ef_core_web_api.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly IClientRepository _clientRepository;

        public TripsController(ITripRepository tripRepository, IClientRepository clientRepository)
        {
            _tripRepository = tripRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Produces(typeof(TripDTO))]
        public IActionResult GetAll()
        {
            var trips = _tripRepository.GetAll().OrderByDescending(t => t.DateFrom);
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients/")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, ClientTripDTO clientTripDTO)
        {
            if(idTrip != clientTripDTO.IdTrip)
            {
                return BadRequest();
            }

            Trip trip = null;
            try
            {
                trip = await _tripRepository.GetAsync(idTrip);
            }
            catch (Exception) { }

            int idClient = -1;

            if (trip == null)
            {
                return new BadRequestObjectResult("Trip with a such an id does not exist");
            }

            var client = new Client()
            {
                FirstName = clientTripDTO.FirstName,
                LastName = clientTripDTO.LastName,
                Email = clientTripDTO.Email,
                Telephone = clientTripDTO.Telephone,
                Pesel = clientTripDTO.Pesel

            };

            if (!await _clientRepository.ContainsAsync(client))
            {
                idClient = await _clientRepository.AddAsync(client);
            }
            else
            {
                idClient = _clientRepository.Get(e => e.Pesel == client.Pesel).First().IdClient;
            }

            if (trip.ClientTrips.Any(ct => ct.IdClient == idClient && ct.IdTrip == trip.IdTrip))
            {
                return StatusCode(409, "Client has been already assigned to the given trip");
            }


            _tripRepository.AssignClient(trip.IdTrip, idClient, clientTripDTO.PaymentDate);
            return Ok();
        }
    }
}
