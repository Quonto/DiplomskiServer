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
            if (user.Username == "")
            {
                return BadRequest("Niste uneli korisničko ime");
            }

            User u = await Context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (u != null)
            {
                return BadRequest("Korisničko ime postoji");

            }
            if (user.Picture == "")
            {
                return BadRequest("Niste uneli sliku");

            }
            if (user.Email == "")
            {
                return BadRequest("Niste uneli mail");

            }
            if (user.Password == "")
            {
                return BadRequest("Niste uneli šifru");
            }
            if (user.UserInformation.NameUser == "")
            {
                return BadRequest("Niste uneli ime");
            }
            if (user.UserInformation.Surename == "")
            {
                return BadRequest("Niste uneli prezime");

            }

            if (user.UserInformation.Phone == "")
            {
                return BadRequest("Niste uneli kontakt");
            }

            User newUser = new User();
            newUser.Delete = false;
            newUser.Email = user.Email;
            newUser.IsAdmin = false;
            newUser.Password = user.Password;
            newUser.Picture = user.Picture;
            newUser.Username = user.Username;

            UserInformation userInformation = new UserInformation();
            userInformation.Data = user.UserInformation.Data;
            userInformation.Date = user.UserInformation.Date;
            userInformation.NameUser = user.UserInformation.NameUser;
            userInformation.Phone = user.UserInformation.Phone;
            userInformation.Surename = user.UserInformation.Surename;
            userInformation.User = newUser;

            newUser.UserInformation = userInformation;

            PlaceProductUser place = new PlaceProductUser();
            place.Name = user.UserInformation.Place.Name;
            place.UserInformation = userInformation;

            newUser.UserInformation.Place = place;

            Context.Users.Add(newUser);
            await Context.SaveChangesAsync();
            return Created("Korisnik je kreiran", newUser);
        }

        [Route("InputUserInformation/{id_user}")]
        [HttpPost]
        public async Task<ActionResult> InputUserInformation(int id_user, [FromBody] UserInformation informations)
        {
            User us = await Context.Users.FindAsync(id_user);
            if (us == null)
            {
                return NotFound();
            }
            informations.User = us;

            Context.UserInformation.Add(informations);
            await Context.SaveChangesAsync();
            return Created("UserInformation created", informations);
        }


        [Route("InputProductPicture/{id_product}")]
        [HttpPost]
        public async Task<ActionResult> InputProductPicture(int id_product, [FromBody] Image productPicture)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return NotFound();
            }
            productPicture.Product = pr;

            Context.Images.Add(productPicture);
            await Context.SaveChangesAsync();
            return Created("ProductPicter created", productPicture);
        }

        [Route("InputReview/{id_product}/{id_user}")]
        [HttpPost]
        public async Task<ActionResult<Review>> InputReview(int id_product, int id_user, [FromBody] Review review)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return NotFound();
            }
            User us = await Context.Users.FindAsync(id_user);
            if (us == null)
            {
                return NotFound();
            }

            Review newReview = new Review();

            if (review.Mark < 1 || review.Mark > 5)
            {
                return BadRequest("Loša Ocena. Ocena mora biti od 1 do 5");
            }

            newReview.Coment = review.Coment;
            newReview.Mark = review.Mark;
            newReview.Product = pr;
            newReview.User = us;

            Context.Reviews.Add(newReview);
            await Context.SaveChangesAsync();


            return newReview;
        }


        [Route("FetchUser")]
        [HttpPost]
        public async Task<ActionResult<User>> FetchUser([FromBody] User user)
        {

            User ko = await Context.Users.Where(u => u.Username == user.Username && u.Delete == false).Include(u => u.UserInformation).Include(u => u.UserInformation).ThenInclude(p => p.Place).FirstOrDefaultAsync();

            if (ko == null)
            {
                return BadRequest("Pogrešno korisnićko ime");
            }

            if (ko.Password != user.Password)
            {
                return BadRequest("Loša šifra");
            }

            if (ko == null)
                return NotFound();

            return ko;
        }

        [Route("CheckPassword")]
        [HttpPost]
        public async Task<ActionResult> CheckPassword([FromBody] User user)
        {
            User us = await Context.Users.FindAsync(user.ID);
            if (us == null)
            {
                return NotFound();
            }

            if (us.Password != user.Password)
            {
                return BadRequest("Pogrešna šifra");
            }

            return Ok();
        }

        [Route("FetchReviews/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> FetchReviews(int id_user)
        {

            List<Review> reviews = await Context.Reviews.Where(u => u.User.ID == id_user).Include(u => u.User).ToListAsync();

            if (reviews.Count == 0)
                return NotFound("Nema komentara");

            return reviews;
        }

        [Route("FetchProductReviews/{id_product}")]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> FetchProductReviews(int id_product)
        {

            List<Review> reviews = await Context.Reviews.Where(u => u.Product.Id == id_product).Include(u => u.User).ToListAsync();

            if (reviews.Count == 0)
                return NotFound("Nema komentara");

            return reviews;
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
                return BadRequest("Korisnik ne postoji");
            }
            return u;
        }

        [Route("FetchAllUsers")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> FetchAllUsers()
        {


            List<User> u = await Context.Users.Where(u => u.IsAdmin == false && u.Delete == false).Include(u => u.UserInformation).Include(p => p.UserInformation).ThenInclude(p => p.Place).ToListAsync();
            if (u.Count == 0)
            {
                return NotFound("Ne postoje korisnici");
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
            if (user.Picture == null)
            {
                return BadRequest("Unesite sliku");
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
                return BadRequest("Uneti mejl već postoji");
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
                return BadRequest("Korisničko ime postoji");
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
                return NotFound();
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

            if (review.Mark < 1 || review.Mark > 5)
            {
                return BadRequest("Loša izmena. Ocena moze biti od 1 do 5");
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

            if (userInformation.Place.Name == "")
            {
                return BadRequest("Niste uneli mesto korisnika");
            }

            if (userInformation.NameUser == "")
            {
                return BadRequest("Niste uneli ime korisnika");
            }

            if (userInformation.Surename == "")
            {
                return BadRequest("Niste uneli prezime korisnika");
            }

            if (userInformation.Phone == "")
            {
                return BadRequest("Niste uneli telefon korisnika");
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
        public async Task<ActionResult> RemovePicture(int id_image)
        {
            Image image = await Context.Images.FindAsync(id_image);
            if (image == null)
            {
                return NotFound();
            }
            Context.Remove(image);
            await Context.SaveChangesAsync();
            return Ok();
        }


        [Route("RemoveReview/{id_review}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveReview(int id_review)
        {
            Review review = await Context.Reviews.FindAsync(id_review);
            if (review == null)
            {
                return NotFound();
            }
            Context.Remove(review);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("RemoveUserInformation/{id_user_information}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveUserInformation(int id_user_information)
        {
            UserInformation userInformation = await Context.UserInformation.FindAsync(id_user_information);
            if (userInformation == null)
            {
                return NotFound();
            }
            Context.Remove(userInformation);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("RemoveUser/{id_user}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveUser(int id_user)
        {
            User user = await Context.Users.FindAsync(id_user);
            if (user == null)
            {
                return NotFound();
            }
            user.Delete = true;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return Ok();
        }


        [Route("RemoveImage/{id_image}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveImage(int id_image)
        {
            Image image = await Context.Images.FindAsync(id_image);
            if (image == null)
            {
                return NotFound();
            }
            Context.Remove(image);
            await Context.SaveChangesAsync();
            return Ok();
        }

    }
}
