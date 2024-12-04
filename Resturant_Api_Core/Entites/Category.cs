using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Entites
{
    public class Category : Base
    {
        public string Name { get; set; }
        ICollection<Meals> Meals { get; set; } = new HashSet<Meals>(); 
    }
}
