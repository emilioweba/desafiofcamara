using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioFCamara.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescricao { get; set; }

        public double ProductPrice { get; set; }
    }
}