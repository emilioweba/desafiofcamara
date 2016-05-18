using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioFCamara.Models
{
    public class HomeViewModel
    {
        public MyToken MyToken { get; set; }

        public List<Product> ProductList { get; set; }
    }
}