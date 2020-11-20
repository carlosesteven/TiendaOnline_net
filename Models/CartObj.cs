using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaOnline.Models
{
    public class CartObj
    {
        public List<Product> Product { get; set; }
        public int Amount { get; set; }
    }
}
