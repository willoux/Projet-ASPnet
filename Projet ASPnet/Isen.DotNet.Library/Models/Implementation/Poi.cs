using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Poi : BaseModel
    {
        public CatPoi Category{ get;set; }
        public Address Address{ get;set; }
        public string Description { get;set; }
        public int? CategoryId { get;set; }
        public int? AddressId { get;set; }
    
        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.description = Description;
            response.category = Category?.ToDynamic();
            response.address = Address?.ToDynamic();
            return response;
        }
       
    }
}