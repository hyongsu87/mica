using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mica.Models;

namespace Mica.ViewModel
{
    public class FormBanksViewModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public Bank Bank { get; set; }
    }
}