using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI_приложение
{
    [Serializable]
    public class Bus_flight
    {
        public string nomber { get; set; }
        public string type { get; set; }
        public string destination { get; set; }
        public DateTime departure_date_and_time { get; set; }
        public DateTime arrivale_date_and_time { get; set; }
    }
}
