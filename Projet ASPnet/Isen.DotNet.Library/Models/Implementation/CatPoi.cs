using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class CatPoi : BaseModel
    {
        public string Description { get;set; }
        public List<Poi> PoiCollection { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.description = Description;
            return response;
        }
    }
}