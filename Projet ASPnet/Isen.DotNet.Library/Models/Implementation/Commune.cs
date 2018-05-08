using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Commune : BaseModel
    {
        public Departement Departement { get;set; }
        public int? DepartementId { get;set; }
        public float Latitude { get;set; }
        public float Longitude { get;set; }

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