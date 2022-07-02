using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Novi.Models
{
    public class UserContext : DbContext
    {

        public DbSet<Korisnik> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }


    }
}