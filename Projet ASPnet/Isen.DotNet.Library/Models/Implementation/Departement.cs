using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Departement : BaseModel
    {
        public int Numero { get;set; }
        public float Long { get;set; }
        public float Lat { get;set; }
    
        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.numero = Numero;
            response.longitude = Long;
            response.latitude = Lat;
            return response;
        }
    }     
}