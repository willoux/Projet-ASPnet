using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Commune : BaseModel
    {
        public Departement Departement { get;set; }
        public List<Address> AddressCollection { get;set; }
        public int? DepartementId { get;set; }
        public String Latitude { get;set; }
        public String Longitude { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.longitude = Longitude;
            response.latitude = Latitude;
            response.depart = Departement?.ToDynamic();
            return response;
        }
    }
}