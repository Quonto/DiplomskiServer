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

        [Route("InputCategory")]
        [HttpPost]
        public async Task<ActionResult<Category>> InputCategory([FromBody] Category category)
        {
            if (category.Name == "")
            {
                return BadRequest("Niste uneli naziv kategorije");
            }

            Category currentCategory = await Context.Categories.Where(c => c.Name == category.Name).FirstOrDefaultAsync();

            if (currentCategory != null)
            {
                currentCategory.Delete = false;
                currentCategory.Picture = category.Picture;
                Context.Categories.Update(currentCategory);
                await Context.SaveChangesAsync();
                return currentCategory;

            }

            Category newCategory = new Category();


            if (category.Picture.Data == null)
            {
                return BadRequest("Niste uneli sliku kategorije");
            }


            newCategory.Delete = false;
            newCategory.Name = category.Name;
            newCategory.Picture = category.Picture;

            Context.Categories.Add(newCategory);
            await Context.SaveChangesAsync();

            return newCategory;
        }


        [Route("FetchAllCategories")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchAllCategories()
        {
            List<Category> c = await Context.Categories.Where(c => c.Delete == false).Include(c => c.Groups).Include(c => c.Groups).ThenInclude(c => c.Products).Include(c => c.Groups).ThenInclude(c => c.Products).ThenInclude(c => c.Picture).Include(c => c.Groups).ThenInclude(p => p.ProductInformation.Where(pi => pi.Delete == false)).Include(c => c.Groups).ThenInclude(c => c.Products).ThenInclude(c => c.Reviews).ToListAsync();

            return c;
        }

        [Route("FetchCategoriesAndGroups")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchCategoriesAndGroups()
        {
            List<Category> c = await Context.Categories.Where(c => c.Delete == false).Include(c => c.Picture).Include(c => c.Groups).Include(g => g.Groups).ThenInclude(pr => pr.ProductInformation.Where(p => p.Delete == false)).Include(g => g.Groups).ThenInclude(p => p.Picture).AsSplitQuery().ToListAsync();

            return c;
        }

        [Route("FetchCategories")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> FetchCategories()
        {
            List<Category> c = await Context.Categories.Where(c => c.Delete == false).Include(p => p.Picture).ToListAsync();
            return c;
        }

        [Route("UpdateCategory")]
        [HttpPut]
        public async Task<ActionResult> UpdateCategory([FromBody] Category category)
        {
            if (category.Name == "")
            {
                return BadRequest("Niste uneli naziv kategorije");
            }

            if (category.Picture.Data == null)
            {
                return BadRequest("Niste uneli sliku kategorije");
            }

            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("RemoveCategory/{id_category}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveCategory(int id_category)
        {
            Category category = await Context.Categories.FindAsync(id_category);
            if (category == null)
            {
                return NotFound();
            }
            category.Delete = true;
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}
