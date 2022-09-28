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
    public class ProductController : ControllerBase
    {

        public CategoryContext Context { get; set; }

        public ProductController(CategoryContext context)
        {
            Context = context;
        }


        [Route("InputNumberOfView/{id_product}")]
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


        [Route("InputNumberOfWish/{id_product}")]
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

        [Route("InputNumberOfLike/{id_product}")]
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


        [Route("FetchProducts/{id_group}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchProducts(int id_group)
        {
            List<Product> pr = await Context.Products.Where(p => p.Group == id_group && p.Buy == false).Include(p => p.Picture).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(p => p.NumberOfViewers).Include(p => p.Reviews).Include(p => p.Place).Include(u => u.User).Include(u => u.User).ThenInclude(ui => ui.UserInformation).Include(p => p.NumberOfWish).Include(l => l.NumberOfLike).AsSplitQuery().ToListAsync();
            return pr;
        }

        [Route("FetchProductName/{id_group}/{product_name}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> FetchProductName(int id_group, string product_name)
        {

            if (product_name == "*")
            {
                return new List<Product>();
            }

            List<Product> pr = await Context.Products.Where(p => p.Group == id_group && p.Buy == false && p.Name.Contains(product_name)).Include(p => p.Picture).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(p => p.NumberOfViewers).Include(p => p.Reviews).Include(p => p.Place).Include(u => u.User).Include(u => u.User).ThenInclude(ui => ui.UserInformation).Include(p => p.NumberOfWish).Include(l => l.NumberOfLike).AsSplitQuery().ToListAsync();
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


        [Route("FetchSingleProduct/{id_product}")]
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

            List<Product> pr = await Context.Products.Where(p => p.Buy == false).OrderByDescending(p => p.NumberOfWish.Count).Include(p => p.NumberOfWish).Include(p => p.Picture).Take(10).AsSplitQuery().ToListAsync();
            return pr;
        }

        [Route("FetchLikeProduct")]
        [HttpGet]
        public async Task<List<Product>> FetchLikeProduct()
        {
            List<Product> pr = await Context.Products.Where(p => p.Buy == false).OrderByDescending(p => p.NumberOfLike.Count).Take(10).Include(p => p.NumberOfLike).Include(p => p.Picture).AsSplitQuery().ToListAsync();
            return pr;
        }

        [Route("FetchNumberOfView/{id_product}")]
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


        [Route("UpdateAuctionProduct")]
        [HttpPut]
        public async Task<ActionResult> UpdateAuctionProduct([FromBody] Product product)
        {
            Product p = await Context.Products.FindAsync(product.Id);

            if (p == null)
            {
                return NotFound();
            }

            p.Buy = product.Buy;
            p.Price = product.Price;
            p.Date = product.Date;
            p.BuyUser = product.BuyUser;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("UpdateProduct")]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            Product p = await Context.Products.FindAsync(product.Id);

            if (p == null)
            {
                return NotFound();
            }

            PlaceProductUser pl = await Context.PlaceProductUser.Where(pl => pl.Product.Id == p.Id).FirstAsync();

            if (pl == null)
            {
                return NotFound();
            }

            pl.Name = product.Place.Name;


            Context.PlaceProductUser.Update(pl);


            p.Name = product.Name;
            p.Price = product.Price;
            p.Phone = product.Phone;
            p.Details = product.Details;
            p.Date = product.Date;

            Context.Products.Update(p);


            List<Image> imageProduct = await Context.Images.Where(p => p.Product.Id == product.Id).ToListAsync();

            foreach (Image img in imageProduct)
            {
                Context.Remove(img);
                await Context.SaveChangesAsync();
            }

            foreach (Image img in product.Picture)
            {
                Image image = new Image();
                image.Data = img.Data;
                image.Name = img.Name;
                image.Product = p;
                Context.Images.Add(image);
                await Context.SaveChangesAsync();
            }




            foreach (ProductInformationData pi in product.Data)
            {
                ProductInformationData pid = await Context.ProductInformationData.FindAsync(pi.Id);

                if (pid == null)
                {
                    return NotFound();
                }

                pid.Data = pi.Data;

                Context.ProductInformationData.Update(pid);

            }
            await Context.SaveChangesAsync();


            Product backProduct = await Context.Products.Where(prod => prod.Id == p.Id).Include(p => p.Data).Include(p => p.Data).ThenInclude(d => d.ProductInformation).Include(p => p.NumberOfLike).Include(p => p.NumberOfViewers).Include(p => p.NumberOfWish).Include(p => p.Picture).Include(p => p.Place).Include(p => p.Reviews).ThenInclude(r => r.User).Include(p => p.User).ThenInclude(ui => ui.UserInformation).ThenInclude(pl => pl.Place).AsSplitQuery().FirstAsync();


            return backProduct;
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

        [Route("RemoveNumberOfLike/{id_like}")]
        [HttpDelete]
        public async Task RemoveNumberOfLike(int id_like)
        {
            NumberOfLike like = await Context.NumberOfLike.FindAsync(id_like);
            Context.Remove(like);
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

    }
}