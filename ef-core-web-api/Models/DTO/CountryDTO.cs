using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ef_core_web_api.Models.DTO
{
    public class CountryDTO
    {
        public CountryDTO(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
