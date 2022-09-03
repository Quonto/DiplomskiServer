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
        public async Task WriteGroup(int id_category, [FromBody] Group groups)
        {
            Category ca = await Context.Categories.FindAsync(id_category);
            groups.Category = ca;

            Context.Groups.Add(groups);
            await Context.SaveChangesAsync();
        }

        [Route("FetchGroups/{id_category}")]
        [HttpGet]
        public async Task<ActionResult<List<Group>>> FetchGroups(int id_category)
        {
            List<Group> g = await Context.Groups.Include(p => p.Picture).Where(c => c.Category.Id == id_category).ToListAsync();
            return g;
        }

        [Route("FetchGroup/{id_group}")]
        [HttpGet]
        public async Task<ActionResult<Group>> FetchGroup(int id_group)
        {
            Group g = await Context.Groups.Where(pi => pi.Id == id_group).Include(p => p.ProductInformation).FirstAsync();
            return g;
        }

        [Route("FetchPopularGroup")]
        [HttpGet]
        public async Task<List<Group>> FetchPopularGroup()
        {
            List<Group> gr = await Context.Groups.OrderByDescending(g => g.Products.Select(p => p.NumberOfViewers.Count).Count()).Include(c => c.Picture).Take(5).AsSplitQuery().ToListAsync();
            return gr;
        }


        [Route("RemoveGroup/{id_group}")]
        [HttpDelete]
        public async Task RemoveGroup(int id_group)
        {
            Group group = await Context.Groups.FindAsync(id_group);
            Context.Remove(group);
            await Context.SaveChangesAsync();
        }

    }
}
