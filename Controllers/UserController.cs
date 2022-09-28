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

        public CategoryContext Context { get; set; }
        public UserController(CategoryContext context)
        {

            Context = context;
        }

        [Route("InputUser")]
        [HttpPost]
        public async Task<ActionResult> InputUser([FromBody] User user)
        {
            User u = await Context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (u != null)
            {
                return BadRequest("User exist");

            }

            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("InputUserInformation/{id_user}")]
        [HttpPost]
        public async Task InputUserInformation(int id_user, [FromBody] UserInformation informations)
        {
            User us = await Context.Users.FindAsync(id_user);
            informations.User = us;

            Context.UserInformation.Add(informations);
            await Context.SaveChangesAsync();
        }


        [Route("InputProductPicture/{id_product}")]
        [HttpPost]
        public async Task InputProductPicture(int id_product, [FromBody] Image productPicture)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            productPicture.Product = pr;

            Context.Images.Add(productPicture);
            await Context.SaveChangesAsync();
        }

        [Route("InputReview/{id_product}/{id_user}")]
        [HttpPost]
        public async Task<Review> InputReview(int id_product, int id_user, [FromBody] Review review)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            User us = await Context.Users.FindAsync(id_user);

            review.Product = pr;
            review.User = us;

            Context.Reviews.Add(review);
            await Context.SaveChangesAsync();

            Review r = await Context.Reviews.Where(r => r.Coment == review.Coment).FirstAsync();
            return r;
        }

        [Route("FetchReviews/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> FetchReviews(int id_user)
        {

            List<Review> reviews = await Context.Reviews.Where(u => u.User.ID == id_user).Include(u => u.User).ToListAsync();

            if (reviews.Count == 0)
                return NotFound();

            return reviews;
        }

        [Route("FetchProductReviews/{id_product}")]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> FetchProductReviews(int id_product)
        {

            List<Review> reviews = await Context.Reviews.Where(u => u.Product.Id == id_product).Include(u => u.User).ToListAsync();

            if (reviews.Count == 0)
                return NotFound();

            return reviews;
        }

        [Route("FetchUser")]
        [HttpPost]
        public async Task<ActionResult<User>> FetchUser([FromBody] User user)
        {

            User ko = await Context.Users.Where(u => u.Username == user.Username && u.Password == user.Password && u.Delete == false).Include(u => u.UserInformation).Include(u => u.UserInformation).ThenInclude(p => p.Place).FirstOrDefaultAsync();

            if (ko == null)
                return NotFound();

            return ko;
        }

        [Route("CheckPassword")]
        [HttpPost]
        public async Task<ActionResult> CheckPassword([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);

            if (us.Password != user.Password)
            {
                return BadRequest();
            }

            return Ok();
        }


        [Route("FetchUser/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<User>> FetchUser(int id_user)
        {
            if (id_user == 0)
            {
                return NotFound();
            }

            User u = await Context.Users.Where(u => u.ID == id_user && u.Delete == false).Include(u => u.UserInformation).Include(p => p.UserInformation).ThenInclude(p => p.Place).FirstAsync();
            if (u == null)
            {
                return BadRequest("Product does not exist");
            }
            return u;
        }

        [Route("FetchAllUsers")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> FetchAllUsers()
        {


            List<User> u = await Context.Users.Where(u => u.IsAdmin == false && u.Delete == false).Include(u => u.UserInformation).Include(p => p.UserInformation).ThenInclude(p => p.Place).ToListAsync();
            if (u == null)
            {
                return BadRequest("Product does not exist");
            }
            return u;
        }

        [Route("UpdateUserPicture")]
        [HttpPut]
        public async Task<ActionResult> UpdateUserPicture([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);

            if (us == null)
            {
                return NotFound();
            }

            us.Picture = user.Picture;

            Context.Users.Update(us);
            await Context.SaveChangesAsync();

            return Ok();
        }


        [Route("UpdateUserMail")]
        [HttpPut]
        public async Task<ActionResult> UpdateUserMail([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);

            if (us == null)
            {
                return NotFound();
            }

            User userMail = await Context.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();

            if (userMail != null)
            {
                return BadRequest();
            }

            us.Email = user.Email;

            Context.Users.Update(us);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("UpdateUserUsername")]
        [HttpPut]
        public async Task<ActionResult> UpdateUserUsername([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);

            if (us == null)
            {
                return NotFound();
            }

            User userUsername = await Context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (userUsername != null)
            {
                return BadRequest();
            }

            us.Username = user.Username;

            Context.Users.Update(us);
            await Context.SaveChangesAsync();

            return Ok();
        }



        [Route("UpdatePassword")]
        [HttpPut]
        public async Task<ActionResult> UpdatePassword([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);

            if (us == null)
            {
                return BadRequest();
            }

            us.Password = user.Password;

            Context.Users.Update(us);
            await Context.SaveChangesAsync();
            return Ok();
        }


        [Route("UpdateReview")]
        [HttpPut]
        public async Task<ActionResult> UpdateReview([FromBody] Review review)
        {
            Review r = await Context.Reviews.FindAsync(review.Id);

            if (r == null)
            {
                return NotFound();
            }

            r.Coment = review.Coment;
            r.Mark = review.Mark;

            Context.Reviews.Update(r);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("UpdateUserProductInformation")]
        [HttpPut]
        public async Task<ActionResult> UpdateUserProductInformation([FromBody] UserInformation userInformation)
        {
            UserInformation ui = await Context.UserInformation.FindAsync(userInformation.Id);

            if (ui == null)
            {
                return NotFound();
            }

            PlaceProductUser pl = await Context.PlaceProductUser.Where(pl => pl.UserInformation.Id == ui.Id).FirstAsync();

            if (pl == null)
            {
                return NotFound();
            }

            pl.Name = userInformation.Place.Name;

            Context.PlaceProductUser.Update(pl);
            await Context.SaveChangesAsync();

            ui.Data = userInformation.Data;
            ui.NameUser = userInformation.NameUser;
            ui.Phone = userInformation.Phone;
            ui.Surename = userInformation.Surename;


            Context.UserInformation.Update(ui);
            await Context.SaveChangesAsync();
            return Ok();
        }


        [Route("RemovePicture/{id_image}")]
        [HttpDelete]
        public async Task RemovePicture(int id_image)
        {
            Image image = await Context.Images.FindAsync(id_image);
            Context.Remove(image);
            await Context.SaveChangesAsync();
        }


        [Route("RemoveReview/{id_review}")]
        [HttpDelete]
        public async Task RemoveReview(int id_review)
        {
            Review review = await Context.Reviews.FindAsync(id_review);
            Context.Remove(review);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveUserInformation/{id_user_information}")]
        [HttpDelete]
        public async Task RemoveUserInformation(int id_user_information)
        {
            UserInformation userInformation = await Context.UserInformation.FindAsync(id_user_information);
            Context.Remove(userInformation);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveUser/{id_user}")]
        [HttpDelete]
        public async Task RemoveUser(int id_user)
        {
            User user = await Context.Users.FindAsync(id_user);

            user.Delete = true;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();

        }


        [Route("RemoveImage/{id_image}")]
        [HttpDelete]
        public async Task RemoveImage(int id_image)
        {
            Image image = await Context.Images.FindAsync(id_image);
            Context.Remove(image);
            await Context.SaveChangesAsync();
        }

    }
}
