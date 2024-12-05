using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Entites
{
    public class Reservison : Base
    {
        public string ReservationName { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationEndTime { get; set; } 
        public bool IsCanceled { get; set; }
        public bool IsCompleted { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
     
    }
}
