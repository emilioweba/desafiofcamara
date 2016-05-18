namespace DesafioFCamara
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DesafioFCamara.Models;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}