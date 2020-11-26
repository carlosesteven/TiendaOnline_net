using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaOnline.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductPicture { get; set; }

        public int ProductPrice { get; set; }

        public int ProductAmount { get; set; }

        public int ProductTotal { get; set; }        

        public string UserId { get; set; }
    }
}
