using Microsoft.AspNetCore.Http;

namespace TiendaOnline.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Units { get; set; }  
    }
}
