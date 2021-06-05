using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mica.Models
{
    public class Account : Parameter
    {
        public string Number { get; set; }
        public Bank Bank { get; set; }
        public int BankId { get; set; }
        public AccountType AccountType { get; set; }
        public int AccountTypeId { get; set; }
        public double Balance { get; set; }
    }
}