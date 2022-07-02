using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Novi.Models;

namespace Novi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public UserContext Context { get; set; }
        private readonly ILogger<UserController> _logger;

        public UserController(UserContext context)
        {
            Context = context;
        }


        [Route("PreuzmiUser")]
        [HttpGet]
        public async Task<List<Korisnik>> PreuzmiKorisnik()
        {

            return await Context.Users.Include(p => p.Products).ToListAsync();
        }


        [Route("Register")]
        [HttpPost]
        public async Task Register([FromBody] Korisnik korisnik)
        {
            Context.Users.Add(korisnik);
            await Context.SaveChangesAsync();
        }

        [Route("FetchUser")]
        [HttpPost]
        public async Task<Korisnik> FetchUser([FromBody] Korisnik korisnik)
        {

            Korisnik ko = await Context.Users.Where(u => u.Username == korisnik.Username && u.Password == korisnik.Password).FirstAsync();
            return ko;
        }

        /*
                [Route("PreuzmiJedanProizvod/{id}")]
                [HttpGet]
                public async Task<List<Product>> PreuzmiJedanProizvod(int id)
                {
                    Product pr = await Context.Products.FindAsync(id);
                    return await Context.Products.Where(pr => pr.ID == id).Include(p => p.Picture).ToListAsync();
                }

                [Route("UpdateProduct")]
                [HttpPut]
                public async Task UpdateProduct([FromBody] Product product)
                {
                    Context.Update<Product>(product);
                    await Context.SaveChangesAsync();
                }

                [Route("DeleteProduct/{id}")]
                [HttpDelete]
                public async Task DeleteProduct(int id)
                {
                    Product product = await Context.Products.FindAsync(id);
                    Context.Remove(product);
                    await Context.SaveChangesAsync();
                }
        */

    }
}
