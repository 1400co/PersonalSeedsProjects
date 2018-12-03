using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmpaLumpaBTC.Common
{
    public class Strategy
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public double BtcBag { get; set; }
        public double Floor { get; set; }
        public double Profit { get; set; }
        public double Discount { get; set; }
    }
}
