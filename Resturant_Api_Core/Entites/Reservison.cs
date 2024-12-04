﻿using System;
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
        public TimeOnly ReservationEndTime { get; set; } 
        public int TableId { get; set; }
        public Table Table { get; set; }
     
    }
}