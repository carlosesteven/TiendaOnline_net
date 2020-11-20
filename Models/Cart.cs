using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaOnline.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartObj> Products { get; set; }
        public string User { get; set; }
    }
}
