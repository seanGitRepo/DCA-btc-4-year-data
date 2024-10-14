using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whatifbtc
{
    internal class User
    {
        public decimal balance { get; set; }
        public string name { get; set; }
     
        public string interval { get; set; }

        public List<calc> calcs = new List<calc>();
        public decimal btcHolding { get; set; }
        public decimal recurringAdd { get; set; }
        public decimal principal { get; set; }
    }
}
