using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Novi.Models
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserInformation> UserInformation { get; set; }

        public DbSet<ProductInformation> ProductInformation { get; set; }

        public DbSet<ProductInformationData> ProductInformationData { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<NumberOfViewe> NumberOfViewes { get; set; }

        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {

        }


    }
}