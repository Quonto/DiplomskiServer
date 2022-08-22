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

            User ko = await Context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).Include(u => u.UserInformation).Include(u => u.UserInformation).ThenInclude(p => p.Place).FirstAsync();

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


        [Route("FetchProducts")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchProducts(int id_group)
        {
            List<Product> pr = await Context.Products.Where(p => p.Group == id_group && p.Buy == false).Include(p => p.Picture).Include(p => p.NumberOfViewers).Include(p => p.Reviews).Include(p => p.Place).Include(u => u.User).Include(u => u.User).ThenInclude(ui => ui.UserInformation).Include(p => p.NumberOfWish).Include(l => l.NumberOfLike).AsSplitQuery().ToListAsync();
            return pr;
        }



        [Route("FetchUserProducts/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchUserProducts(int id_user)
        {
            List<Product> pr = await Context.Products.Where(p => p.User.ID == id_user && p.Buy == true).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(r => r.Reviews).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).ToListAsync();
            return pr;
        }


        [Route("GetUserProducts/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetUserProducts(int id_user)
        {
            List<Product> pr = await Context.Products.Where(p => p.User.ID == id_user).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(r => r.Reviews).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).Include(p => p.Place).AsSplitQuery().ToListAsync();
            return pr;
        }


        [Route("FetchSingleProduct")]
        [HttpGet]
        public async Task<ActionResult<Product>> FetchSingleProduct(int id_product)
        {
            Product pr = await Context.Products.Where(p => p.Id == id_product).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(p => p.Place).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(u => u.User).ThenInclude(ui => ui.UserInformation).ThenInclude(pl => pl.Place).Include(r => r.Reviews).Include(r => r.Reviews).ThenInclude(u => u.User).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).AsSplitQuery().FirstAsync();
            return pr;
        }

        [Route("FetchMostWanted")]
        [HttpGet]
        public async Task<List<Product>> FetchMostWanted()
        {

            List<Product> pr = await Context.Products.Where(p => p.Buy == false).OrderByDescending(p => p.NumberOfWish.Count).Include(p => p.NumberOfWish).Include(p => p.Picture).Take(20).AsSplitQuery().ToListAsync();
            return pr;
        }

        [Route("FetchLikeProduct")]
        [HttpGet]
        public async Task<List<Product>> FetchLikeProduct()
        {
            List<Product> pr = await Context.Products.Where(p => p.Buy == false).OrderByDescending(p => p.NumberOfLike.Count).Take(20).Include(p => p.NumberOfLike).Include(p => p.Picture).AsSplitQuery().ToListAsync();
            return pr;
        }

        [Route("FetchPopularGroup")]
        [HttpGet]
        public async Task<List<Group>> FetchPopularGroup()
        {
            List<Group> gr = await Context.Groups.OrderByDescending(g => g.Products.Select(p => p.NumberOfViewers.Count).Count()).Include(c => c.Picture).Take(5).AsSplitQuery().ToListAsync();
            return gr;
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

        [Route("FetchUser/{id_user}")]
        [HttpGet]
        public async Task<ActionResult<User>> FetchUser(int id_user)
        {
            if (id_user == 0)
            {
                return NotFound();
            }

            User u = await Context.Users.Where(u => u.ID == id_user).Include(u => u.UserInformation).Include(p => p.UserInformation).ThenInclude(p => p.Place).FirstAsync();
            if (u == null)
            {
                return BadRequest("Product does not exist");
            }
            return u;
        }


        [Route("InputPurchase/{id_product}")]
        [HttpPut]
        public async Task InputPurchase(int id_product, [FromBody] Product product)
        {
            Product p = await Context.Products.FindAsync(id_product);
            p.AddToCart = product.AddToCart;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();



        }

        [Route("InputBuy/{id_user}")]
        [HttpPut]
        public async Task InputBuy(int id_user, [FromBody] List<Product> products)
        {

            foreach (Product product in products)
            {
                Product p = await Context.Products.FindAsync(product.Id);
                if (p.Buy != true && p != null)
                {
                    p.Buy = true;
                    if (id_user != 0)
                    {
                        p.BuyUser = id_user;
                    }
                    Context.Products.Update(p);
                }
            }
            await Context.SaveChangesAsync();
        }

        [Route("InputProduct/{id_product}")]
        [HttpPut]
        public async Task InputProduct(int id_product)
        {
            Product p = await Context.Products.FindAsync(id_product);
            p.AddToCart = false;
            p.Buy = false;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();
        }

        [Route("UpdateProduct")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            Product p = await Context.Products.FindAsync(product.Id);

            if (p == null)
            {
                return NotFound();
            }

            p.Buy = product.Buy;
            p.Price = product.Price;
            p.Date = product.Date;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("UpdateAuction")]
        [HttpPut]
        public async Task<ActionResult> UpdateAuction([FromBody] Auction auction)
        {
            Auction au = await Context.Auction.FindAsync(auction.Id);

            if (au == null)
            {
                return NotFound();
            }

            au.Time = auction.Time;
            au.MinimumPrice = auction.MinimumPrice;
            au.User = auction.User;


            Context.Auction.Update(au);
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


        [Route("InputPlace")]
        [HttpPost]
        public async Task<ActionResult<int>> InputPlace([FromBody] Place place)
        {
            Context.Place.Add(place);
            await Context.SaveChangesAsync();

            Place p = await Context.Place.Where(p => p.Name == place.Name).FirstAsync();
            return p.Id;
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

        [Route("InputProductBuy/{id_user}/{id_group}")]
        [HttpPost]
        public async Task<ActionResult<int>> InputProductBuy(int id_user, int id_group, [FromBody] Product product)
        {
            User u = await Context.Users.FindAsync(id_user);

            if (u == null)
            {
                return NotFound();
            }


            Group gr = await Context.Groups.FindAsync(id_group);
            if (gr == null)
            {
                return NotFound();
            }

            for (var i = 0; i < product.Data.Count; i++)
            {
                ProductInformation pr = await Context.ProductInformation.FindAsync(product.Data[i].IdInfo);

                if (pr == null)
                {
                    return NotFound();
                }
                product.Data[i].ProductInformation = pr;
            }

            product.User = u;
            product.Group = gr.Id;


            Context.Products.Add(product);
            await Context.SaveChangesAsync();

            Product ppr = Context.Products.Where(pr => pr.User == u && pr.Group == gr.Id && pr.Name == product.Name).FirstOrDefault();

            if (ppr == null)
            {
                return NotFound();
            }

            return ppr.Id;
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
        public async Task<IActionResult> RemoveProduct(int id_product)
        {
            Product product = await Context.Products.Where(p => p.Id == id_product).Include(p => p.NumberOfWish).Include(p => p.NumberOfLike).Include(p => p.NumberOfViewers).Include(p => p.Picture).Include(p => p.Reviews).Include(p => p.Data).Include(p => p.Place).FirstAsync();

            if (product == null)
            {
                return NotFound();
            }

            foreach (NumberOfWish wish in product.NumberOfWish)
            {
                Context.NumberOfWish.Remove(wish);
            }

            foreach (NumberOfLike like in product.NumberOfLike)
            {
                Context.NumberOfLike.Remove(like);
            }

            foreach (NumberOfViewe viewe in product.NumberOfViewers)
            {
                Context.NumberOfViewes.Remove(viewe);
            }

            foreach (Image picture in product.Picture)
            {
                Context.Images.Remove(picture);
            }

            foreach (Review review in product.Reviews)
            {
                Context.Reviews.Remove(review);
            }

            foreach (ProductInformationData data in product.Data)
            {
                Context.ProductInformationData.Remove(data);
            }

            Context.PlaceProductUser.Remove(product.Place);


            if (product.Auction == true)
            {
                Auction au = await Context.Auction.Where(a => a.Product == product.Id).FirstAsync();
                Context.Auction.Remove(au);
            }

            Context.Remove(product);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("RemoveNumberOfLike/{id_like}")]
        [HttpDelete]
        public async Task RemoveNumberOfLike(int id_like)
        {
            NumberOfLike like = await Context.NumberOfLike.FindAsync(id_like);
            Context.Remove(like);
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
