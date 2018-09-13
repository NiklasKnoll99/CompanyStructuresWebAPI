using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyStructuresWebAPI.Model
{
    public class Company
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string CountryCode { get; set; }

        public string ProvinceName { get; set; }

        public string PostCode { get; set; }

        public string CityName { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }
    }
}
