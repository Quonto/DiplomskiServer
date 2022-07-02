using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Novi.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Korisnik> Users { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }


    }
}