using ef_core_web_api.Models.DTO;
using System;
using System.Collections.Generic;

namespace ef_core_web_api.Models
{
    public class TripDTO
    {
        public TripDTO(string name, string description, DateTime dateFrom, DateTime dateTo, int maxPeople, IEnumerable<CountryDTO> countriesDto, IEnumerable<ClientDTO> clientsDto)
        {
            Name = name;
            Description = description;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MaxPeople = maxPeople;

            Countries = countriesDto;
            Clients = clientsDto;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
        public int MaxPeople { get; }

        public IEnumerable<CountryDTO> Countries { get; }
        public IEnumerable<ClientDTO> Clients { get; }
    }
}
