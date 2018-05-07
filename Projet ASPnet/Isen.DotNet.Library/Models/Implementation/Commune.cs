using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Commune : BaseModel
    {
        public Departement Departement { get;set; }
        public float Lat { get;set; }
        public float Long { get;set; }
    }
}