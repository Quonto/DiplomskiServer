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

        [Route("FetchUser")]
        [HttpPost]
        public async Task<ActionResult<User>> FetchUser([FromBody] User user)
        {

            User ko = await Context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).Include(u => u.UserInformation).FirstAsync();

            if (ko == null)
                return NotFound();

            return ko;
        }

        [Route("FetchProductInformation")]
        [HttpGet]
        public async Task<ActionResult<List<ProductInformation>>> FetchProductInformation(int id_group)
        {
            List<ProductInformation> pr = await Context.ProductInformation.Where(g => g.Groups.Id == id_group).ToListAsync();
            return pr;
        }


        [Route("FetchProduct")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchProduct(int id_group)
        {
            List<Product> pr = await Context.Products.Where(p => p.Group.Id == id_group).Include(p => p.Data).Include(p => p.Data).ThenInclude(p => p.ProductInformation).Include(p => p.Picture).Include(p => p.NumberOfViewers).Include(p => p.Reviews).Include(p => p.Reviews).ThenInclude(r => r.User).Include(u => u.User).Include(u => u.User).ThenInclude(ui => ui.UserInformation).Include(p => p.NumberOfWish).Include(l => l.NumberOfLike).ToListAsync();
            return pr;
        }

        [Route("FetchUserProducts/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchUserProducts(int id_user)
        {
            List<Product> pr = await Context.Products.Where(p => p.User.ID == id_user).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(r => r.Reviews).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).ToListAsync();
            return pr;
        }


        [Route("FetchSingleProduct")]
        [HttpGet]
        public async Task<ActionResult<Product>> FetchSingleProduct(int id_product)
        {
            Product pr = await Context.Products.Where(p => p.Id == id_product).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(r => r.Reviews).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).FirstAsync();
            return pr;
        }

        [Route("FetchNumberOfView")]
        [HttpGet]
        public async Task<ActionResult<List<NumberOfViewe>>> FetchNumberOfView(int id_product)
        {
            Product pd = await Context.Products.FindAsync(id_product);
            if (pd == null)
            {
                return BadRequest("Product does not exist");
            }
            return pd.NumberOfViewers;
        }

        [Route("InputUser")]
        [HttpPost]
        public async Task InputUser([FromBody] User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
        }

        [Route("InputUserInformation")]
        [HttpPost]
        public async Task InputUserInformation(int id_user, [FromBody] UserInformation informations)
        {
            User us = await Context.Users.FindAsync(id_user);
            informations.User = us;

            Context.UserInformation.Add(informations);
            await Context.SaveChangesAsync();
        }

        [Route("InputProduct")]
        [HttpPost]
        public async Task<ActionResult<int>> InputProduct(int id_user, int id_group, [FromBody] Product product)
        {
            User us = await Context.Users.FindAsync(id_user);

            if (us == null)
            {
                return NotFound();
            }

            Group gr = await Context.Groups.FindAsync(id_group);

            if (gr == null)
            {
                return NotFound();
            }

            product.User = us;
            product.Group = gr;

            for (var i = 0; i < product.Data.Count; i++)
            {
                ProductInformation pr = await Context.ProductInformation.FindAsync(product.Data[i].IdInfo);
                if (pr == null)
                {
                    return NotFound();
                }
                product.Data[i].ProductInformation = pr;
            }

            Context.Products.Add(product);
            await Context.SaveChangesAsync();


            Product ppr = Context.Products.Where(pr => pr.User == us && pr.Group == gr && pr.Name == product.Name).FirstOrDefault();

            if (ppr == null)
            {
                return NotFound();
            }

            return ppr.Id;
        }

        [Route("InputProductInformation")]
        [HttpPost]
        public async Task<ActionResult<int>> InputProductInformation(int id_group, [FromBody] ProductInformation productInformation)
        {
            Group gr = await Context.Groups.FindAsync(id_group);
            if (gr == null)
            {
                return NotFound();
            }

            productInformation.Groups = gr;

            Context.ProductInformation.Add(productInformation);
            await Context.SaveChangesAsync();

            ProductInformation pr = Context.ProductInformation.Where(pr => pr.Name == productInformation.Name).FirstOrDefault();
            return pr.Id;
        }

        [Route("InputNumberOfView")]
        [HttpPost]
        public async Task<ActionResult> InputNumberOfViewe(int id_product, [FromBody] NumberOfViewe numberOfViewe)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return BadRequest("Product does not exist");
            }

            NumberOfViewe nv = await Context.NumberOfViewes.Where(v => v.IdUser == numberOfViewe.IdUser && v.Product.Id == id_product).FirstOrDefaultAsync();

            if (nv == null)
            {
                numberOfViewe.Product = pr;
                Context.NumberOfViewes.Add(numberOfViewe);
                await Context.SaveChangesAsync();
            }

            return Ok();
        }

        [Route("InputNumberOfWish")]
        [HttpPost]
        public async Task<ActionResult<int>> InputNumberOfWish(int id_product, [FromBody] NumberOfWish numberOfWish)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return BadRequest("Product does not exist");
            }

            NumberOfWish nv = await Context.NumberOfWish.Where(v => v.IdUser == numberOfWish.IdUser && v.Product.Id == id_product).FirstOrDefaultAsync();

            if (nv == null)
            {
                numberOfWish.Product = pr;
                Context.NumberOfWish.Add(numberOfWish);
                await Context.SaveChangesAsync();
            }
            NumberOfWish nw = await Context.NumberOfWish.Where(p => p.Product == pr && p.IdUser == numberOfWish.IdUser).FirstAsync();

            return nw.Id;
        }

        [Route("InputNumberOfLike")]
        [HttpPost]
        public async Task<ActionResult<int>> InputNumberOfLike(int id_product, [FromBody] NumberOfLike numberOfLike)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return BadRequest("Product does not exist");
            }

            NumberOfLike nv = await Context.NumberOfLike.Where(v => v.IdUser == numberOfLike.IdUser && v.Product.Id == id_product).FirstOrDefaultAsync();

            if (nv == null)
            {
                numberOfLike.Product = pr;
                Context.NumberOfLike.Add(numberOfLike);
                await Context.SaveChangesAsync();
            }

            NumberOfLike nw = await Context.NumberOfLike.Where(p => p.Product == pr && p.IdUser == numberOfLike.IdUser).FirstAsync();

            return nw.Id;
        }

        [Route("InputProductPicture")]
        [HttpPost]
        public async Task InputProductPicture(int id_product, [FromBody] Image productPicture)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            productPicture.Product = pr;

            Context.Images.Add(productPicture);
            await Context.SaveChangesAsync();
        }

        [Route("InputReview")]
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


        [Route("RemovePicture/{id_image}")]
        [HttpDelete]
        public async Task RemovePicture(int id_image)
        {
            Image image = await Context.Images.FindAsync(id_image);
            Context.Remove(image);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveNumberOfView/{id_view}")]
        [HttpDelete]
        public async Task RemoveNumberOfView(int id_view)
        {
            NumberOfViewe viewe = await Context.NumberOfViewes.FindAsync(id_view);
            Context.Remove(viewe);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveNumberOfWish/{id_wish}")]
        [HttpDelete]
        public async Task RemoveNumberOfWish(int id_wish)
        {
            NumberOfWish wish = await Context.NumberOfWish.FindAsync(id_wish);
            Context.Remove(wish);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveProductInformationData/{id_product_information_data}")]
        [HttpDelete]
        public async Task RemoveProductInformaionData(int id_product_information_data)
        {
            ProductInformationData pid = await Context.ProductInformationData.FindAsync(id_product_information_data);
            Context.Remove(pid);
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
            Context.Remove(user);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveProduct/{id_product}")]
        [HttpDelete]
        public async Task RemoveProduct(int id_product)
        {
            Product product = await Context.Products.FindAsync(id_product);
            Context.Remove(product);
            await Context.SaveChangesAsync();
        }

        [Route("RemoveNumberOfLike/{id_like}")]
        [HttpDelete]
        public async Task RemoveNumberOfLike(int id_like)
        {
            NumberOfLike like = await Context.NumberOfLike.FindAsync(id_like);
            Context.Remove(like);
            await Context.SaveChangesAsync();
        }


        /*
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
        */

    }
}
