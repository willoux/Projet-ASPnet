using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Address : BaseModel
    {
        public string Text { get;set; }
        public int Zipcode { get;set; }
        public Commune Commune { get;set; }
        public Double Longitude { get;set; }
        public Double Latitude { get;set; }
        public int? CommuneId { get;set; }
        public List<Poi> PoiCollection { get;set; }


        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.text = Text;
            response.zipcode = Zipcode;
            response.commune = Commune?.ToDynamic();
            response.longitude = Longitude;
            response.latitude = Latitude;
            return response;
        }
    }
}