using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Entites.Order_Mangment
{
    public class MealsInOrder
    {
        public MealsInOrder()
        {
            
        }
        public MealsInOrder(int id , string name ,string pic)
        {
            this.Id = id;
            this.Name = name;
            this.Picture = pic;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}
