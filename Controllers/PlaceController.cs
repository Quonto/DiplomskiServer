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
            if (place.Name == "")
            {
                return BadRequest("Niste uneli naziv mesta");
            }


            Place currentPlace = await Context.Place.Where(p => p.Name == place.Name).FirstOrDefaultAsync();

            if (currentPlace != null)
            {
                currentPlace.Delete = false;
                Context.Place.Update(currentPlace);
                await Context.SaveChangesAsync();
                return currentPlace.Id;
            }


            Place newPlace = new Place();
            newPlace.Delete = false;
            newPlace.Name = place.Name;

            Context.Place.Add(newPlace);
            await Context.SaveChangesAsync();


            return newPlace.Id;
        }


        [Route("FetchPlace")]
        [HttpGet]
        public async Task<ActionResult<List<Place>>> FetchPlace()
        {
            List<Place> p = await Context.Place.Where(p => p.Delete == false).AsSplitQuery().ToListAsync();
            if (p.Count == 0)
            {
                return NotFound("Nema mesta");
            }
            return p;
        }


        [Route("UpdatePlace")]
        [HttpPut]
        public async Task<ActionResult> UpdatePlace([FromBody] Place place)
        {
            if (place.Name == "")
            {
                return BadRequest("Niste uneli naziv mesta");
            }

            Place curentPlace = await Context.Place.Where(p => p.Id == place.Id).FirstAsync();

            if (curentPlace == null)
            {
                return NotFound();
            }

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

        [Route("RemovePlace/{id_place}")]
        [HttpDelete]
        public async Task<ActionResult> RemovePlace(int id_place)
        {
            Place p = await Context.Place.FindAsync(id_place);
            if (p == null)
            {
                return NotFound();
            }
            p.Delete = true;

            Context.Place.Update(p);
            await Context.SaveChangesAsync();
            return Ok();
        }




    }
}