using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Departement : BaseModel
    {
        public List<Commune> CommuneCollection { get;set; }
        public int Numero { get;set; }
        public Double Longitude { get;set; }
        public Double Latitude { get;set; }
    
        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.numero = Numero;
            response.longitude = Longitude;
            response.latitude = Latitude;
            return response;
        }
    }     
}