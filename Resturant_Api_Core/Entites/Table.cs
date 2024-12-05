using Resturant_Api_Core.Entites.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Entites
{
    public class Table : Base
    {
        public int Capacity { get; set; }
        public SmokingPremissions smoking { get; set; }
        public TableStatus IsAvailable { get; set; }
        public string Floor { get; set; }
        public int? ReservationId { get; set; }
        public Reservison Reservation { get; set; }
    }
}
