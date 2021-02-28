using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mica.Models
{
    public class Transaction : Model
    {
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}