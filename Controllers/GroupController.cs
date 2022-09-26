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
    public class GroupController : ControllerBase
    {

        public CategoryContext Context { get; set; }

        public GroupController(CategoryContext context)
        {
            Context = context;
        }

        [Route("WriteGroup/{id_category}")]
        [HttpPost]
        public async Task<ActionResult<Group>> WriteGroup(int id_category, [FromBody] Group groups)
        {

            Category ca = await Context.Categories.FindAsync(id_category);

            Group gr = await Context.Groups.Where(g => g.Name == groups.Name && g.Category.Name == ca.Name).FirstOrDefaultAsync();

            if (gr != null)
            {
                gr.Delete = false;
                gr.Picture = groups.Picture;
                gr.ProductInformation = groups.ProductInformation;
                gr.Products = groups.Products;




                Context.Groups.Update(gr);
                await Context.SaveChangesAsync();
                return gr;
            }


            Group g = new Group();
            g.Delete = false;
            g.Name = groups.Name;
            g.Picture = groups.Picture;
            g.Category = ca;

            Context.Groups.Add(g);
            await Context.SaveChangesAsync();
            return g;
        }

        [Route("FetchGroups/{id_category}")]
        [HttpGet]
        public async Task<ActionResult<List<Group>>> FetchGroups(int id_category)
        {
            List<Group> g = await Context.Groups.Where(g => g.Delete == false).Include(p => p.Picture).Where(c => c.Category.Id == id_category).ToListAsync();
            return g;
        }

        [Route("FetchGroup/{id_group}")]
        [HttpGet]
        public async Task<ActionResult<Group>> FetchGroup(int id_group)
        {
            Group g = await Context.Groups.Where(pi => pi.Id == id_group && pi.Delete == false).Include(p => p.ProductInformation.Where(p => p.Delete == false)).FirstAsync();
            return g;
        }

        [Route("FetchPopularGroup")]
        [HttpGet]
        public async Task<List<Group>> FetchPopularGroup()
        {
            List<Group> gr = await Context.Groups.Where(g => g.Delete == false).OrderByDescending(g => g.Products.Select(p => p.NumberOfViewers.Count).Count()).Include(c => c.Picture).Take(5).AsSplitQuery().ToListAsync();
            return gr;
        }

        [Route("UpdateGroup")]
        [HttpPut]
        public async Task<ActionResult> UpdateGroup([FromBody] Group group)
        {
            Context.Groups.Update(group);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("RemoveGroup/{id_group}")]
        [HttpDelete]
        public async Task RemoveGroup(int id_group)
        {
            Group group = await Context.Groups.FindAsync(id_group);

            group.Delete = true;
            Context.Groups.Update(group);
            await Context.SaveChangesAsync();
        }

    }
}
