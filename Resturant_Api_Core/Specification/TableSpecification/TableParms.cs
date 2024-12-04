﻿using Resturant_Api_Core.Entites.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.TableSpecification
{
    public class TableParms
    {
        private const int MaxPageSize = 10; 
        private int pageSize = 5;
       public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
        public int? TableCapacity { get; set; }
        public TableStatus? IsAvalible { get; set; }
    }
}
