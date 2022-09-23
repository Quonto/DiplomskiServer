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
    public class PlaceController : ControllerBase
    {
        public CategoryContext Context { get; set; }

        public PlaceController(CategoryContext context)
        {
            Context = context;
        }


        [Route("InputPlace")]
        [HttpPost]
        public async Task<ActionResult<int>> InputPlace([FromBody] Place place)
        {
            Context.Place.Add(place);
            await Context.SaveChangesAsync();

            Place p = await Context.Place.Where(p => p.Name == place.Name).FirstAsync();
            return p.Id;
        }


        [Route("FetchPlace")]
        [HttpGet]
        public async Task<ActionResult<List<Place>>> FetchPlace()
        {
            List<Place> p = await Context.Place.AsSplitQuery().ToListAsync();
            return p;
        }

        [Route("UpdatePlace")]
        [HttpPut]
        public async Task<ActionResult> UpdatePlace([FromBody] Place place)
        {

            Place curentPlace = await Context.Place.Where(p => p.Id == place.Id).FirstAsync();
            if (curentPlace == null)
            { return NotFound(); }

            List<PlaceProductUser> places = await Context.PlaceProductUser.Where(p => p.Name == curentPlace.Name && p.UserInformation == null).ToListAsync();

            foreach (PlaceProductUser p in places)
            {
                p.Name = place.Name;
                Context.PlaceProductUser.Update(p);

            }
            curentPlace.Name = place.Name;

            Context.Place.Update(curentPlace);
            await Context.SaveChangesAsync();

            return Ok();
        }



        [Route("UpdateUserPlace/{id_place}")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateUserPlace(int id_place, [FromBody] User user)
        {
            PlaceProductUser p = await Context.PlaceProductUser.FindAsync(id_place);
            if (p == null)
            {
                return NotFound();
            }

            user.UserInformation.Place = p;

            Context.Users.Update(user);
            await Context.SaveChangesAsync();

            return Ok();
        }





    }
}