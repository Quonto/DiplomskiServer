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
        private readonly ILogger<UserController> _logger;

        public UserController(CategoryContext context)
        {
            Context = context;
        }

        [Route("FetchUser")]
        [HttpPost]
        public async Task<ActionResult<User>> FetchUser([FromBody] User user)
        {

            User ko = await Context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).Include(u => u.UserInformation).Include(p => p.Products).Include(p => p.Products).ThenInclude(g => g.Group).ThenInclude(pi => pi.ProductInformation).Include(p => p.Products).ThenInclude(r => r.Reviews).FirstAsync();

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
            List<Product> pr = await Context.Products.Where(p => p.Group.Id == id_group).Include(p => p.Data).Include(p => p.Data).ThenInclude(p => p.ProductInformation).Include(p => p.Picture).Include(p => p.NumberOfViewers).Include(p => p.Reviews).Include(u => u.User).ToListAsync();
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
        public async Task<IActionResult> InputNumberOfViewe(int id_product, [FromBody] NumberOfViewe numberOfViewe)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            if (pr == null)
            {
                return BadRequest("Product does not exist");
            }

            numberOfViewe.Product = pr;
            Context.NumberOfViewes.Add(numberOfViewe);
            await Context.SaveChangesAsync();

            return Ok();
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
        public async Task InputReview(int id_product, [FromBody] Review review)
        {
            Product pr = await Context.Products.FindAsync(id_product);
            review.Product = pr;

            Context.Reviews.Add(review);
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
