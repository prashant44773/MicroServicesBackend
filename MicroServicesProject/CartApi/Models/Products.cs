using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public class Products
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public int ReqID { get; set; }
    }
}
