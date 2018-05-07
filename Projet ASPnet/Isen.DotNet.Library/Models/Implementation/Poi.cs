using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Poi : BaseModel
    {
        public string Description { get;set; }
        public CatPoi Category{ get;set; }
        public Address Address{ get;set; }
    
       
    }
}