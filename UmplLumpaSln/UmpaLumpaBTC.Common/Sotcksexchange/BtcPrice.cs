using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmpaLumpaBTC.Common.Sotcksexchange
{
    public class BtcPrice
    {

        public string buy { get; set; }
        public string sell { get; set; }
        public string market_name { get; set; }
        public int updated_time { get; set; }
        public int server_time { get; set; }
    }
}
