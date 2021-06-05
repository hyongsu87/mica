using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mica.Models
{
    public class Bank : Parameter
    {
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}