using System;
using System.Collections.Generic;

#nullable disable

namespace ef_core_web_api.Models
{
    public partial class Country : EntityBase
    {
        public Country()
        {
            CountryTrips = new HashSet<CountryTrip>();
        }

        public int IdCountry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    }
}
