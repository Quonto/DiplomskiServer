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
    public class ProductController : ControllerBase
    {

        public ProductContext Context { get; set; }
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductContext context)
        {
            Context = context;
        }


        [Route("PreuzmiProizvod")]
        [HttpGet]
        public async Task<List<Product>> PreuzmiProizvod()
        {

            return await Context.Products.Include(p => p.Picture).ToListAsync();
        }


        [Route("UpisiProduct")]
        [HttpPost]
        public async Task<int> UpisiProizvod([FromBody] Product proizvod)
        {
            Context.Products.Add(proizvod);
            await Context.SaveChangesAsync();
            Product pr = await Context.Products.Where(p => p.Naziv == proizvod.Naziv).FirstAsync();
            return pr.ID;
        }


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


    }
}
