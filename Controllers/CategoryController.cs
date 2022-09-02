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
            List<Category> c = await Context.Categories.Include(c => c.Picture).Include(c => c.Groups).Include(g => g.Groups).ThenInclude(pr => pr.ProductInformation).AsSplitQuery().ToListAsync();
            return c;
        }

        [Route("FetchCategories")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchCategories()
        {
            List<Category> c = await Context.Categories.Include(p => p.Picture).ToListAsync();
            return c;
        }


        [Route("FetchPlace")]
        [HttpGet]
        public async Task<ActionResult<List<Place>>> FetchPlace()
        {
            List<Place> p = await Context.Place.AsSplitQuery().ToListAsync();
            return p;
        }


        [Route("WriteCategory")]
        [HttpPost]
        public async Task WriteCategory([FromBody] Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
        }


        [Route("UpdateCategory")]
        [HttpPut]
        public async Task UpdateCategory([FromBody] Category category)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveCategory/{id_category}")]
        [HttpDelete]
        public async Task RemoveCategory(int id_category)
        {
            Category category = await Context.Categories.FindAsync(id_category);
            Context.Remove(category);
            await Context.SaveChangesAsync();
        }
    }
}
