using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Departement : BaseModel
    {
        public List<Commune> CommuneCollection { get;set; }
        public int? CommuneCount => CommuneCollection?.Count;
        public int Numero { get;set; }
        public float Long { get;set; }
        public float Lat { get;set; }
    
        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nb = CommuneCount;
            response.numero = Numero;
            response.longitude = Long;
            response.latitude = Lat;
            return response;
        }
    }     
}