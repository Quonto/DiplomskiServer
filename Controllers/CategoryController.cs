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
    public class CategoryController : ControllerBase
    {

        public CategoryContext Context { get; set; }

        public CategoryController(CategoryContext context)
        {
            Context = context;
        }


        [Route("FetchAllCategories")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchAllCategories()
        {
            List<Category> c = await Context.Categories.Include(c => c.Groups).Include(c => c.Groups).ThenInclude(c => c.Products).Include(c => c.Groups).ThenInclude(c => c.Products).ThenInclude(c => c.Picture).Include(c => c.Groups).ThenInclude(p => p.ProductInformation).Include(g => g.Groups).ThenInclude(p => p.ProductInformation).Include(c => c.Groups).ThenInclude(c => c.Products).ThenInclude(c => c.Reviews).ToListAsync();
            if (c == null)
            {
                return NotFound();
            }
            return c;
        }

        [Route("FetchCategoriesAndGroups")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchCategoriesAndGroups()
        {
            List<Category> c = await Context.Categories.Include(c => c.Groups).Include(g => g.Groups).ThenInclude(pr => pr.ProductInformation).ToListAsync();
            return c;
        }

        [Route("FetchCategories")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchCategories()
        {
            List<Category> c = await Context.Categories.Include(p => p.Picture).ToListAsync();
            return c;
        }

        [Route("FetchGroups")]
        [HttpGet]
        public async Task<ActionResult<List<Group>>> FetchGroups(int id_category)
        {
            List<Group> g = await Context.Groups.Include(p => p.Picture).Where(c => c.Category.Id == id_category).ToListAsync();
            return g;
        }

        [Route("FetchGroup")]
        [HttpGet]
        public async Task<ActionResult<Group>> FetchGroup(int id_group)
        {
            Group g = await Context.Groups.Where(pi => pi.Id == id_group).Include(p => p.ProductInformation).FirstAsync();
            return g;
        }

        [Route("WriteCategory")]
        [HttpPost]
        public async Task WriteCategory([FromBody] Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
        }

        [Route("WriteGroup")]
        [HttpPost]
        public async Task WriteGroup(int id_category, [FromBody] Group groups)
        {
            Category ca = await Context.Categories.FindAsync(id_category);
            groups.Category = ca;

            Context.Groups.Add(groups);
            await Context.SaveChangesAsync();
        }


        [Route("UpdateCategory")]
        [HttpPost]
        public async Task UpdateCategory([FromBody] Category category)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
        }

        /*
                [Route("PreuzmiJedanProizvod/{id}")]
                [HttpGet]
                public async Task<List<Product>> PreuzmiJedanProizvod(int id)
                {
                    Product pr = await Context.Products.FindAsync(id);
                    return await Context.Products.Where(pr => pr.Id == id).Include(p => p.Picture).ToListAsync();
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
