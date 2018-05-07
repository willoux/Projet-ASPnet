using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Address : BaseModel
    {
        public string Text { get;set; }
        public int Zipcode { get;set; }
        public Commune Commune { get;set; }
        public float Long { get;set; }
        public float Lat { get;set; }
    }
}