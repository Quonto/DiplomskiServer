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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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

            Product newProduct = new Product();

            newProduct.User = u;
            newProduct.Group = gr.Id;

            List<ProductInformationData> prData = new List<ProductInformationData>();

            for (var i = 0; i < product.Data.Count; i++)
            {

                ProductInformation pr = await Context.ProductInformation.FindAsync(product.Data[i].IdInfo);

                if (pr == null)
                {
                    return NotFound();
                }

                ProductInformationData prod = new ProductInformationData();

                prod.Data = product.Data[i].Data;


                prod.IdInfo = product.Data[i].IdInfo;

                prod.ProductInformation = pr;

                prod.Product = newProduct;

                prData.Add(prod);
            }

            List<Image> images = new List<Image>();

            for (var i = 0; i < product.Picture.Count; i++)
            {


                Image image = new Image();

                image = product.Picture[i];

                images.Add(image);
            }



            PlaceProductUser place = new PlaceProductUser();
            place = product.Place;

            newProduct.Data = prData;
            newProduct.Name = product.Name;
            newProduct.Date = product.Date;
            newProduct.Details = product.Details;
            newProduct.Phone = product.Phone;
            newProduct.Price = product.Price;
            newProduct.Auction = product.Auction;
            newProduct.Place = place;
            newProduct.Picture = images;

            Context.Products.Add(newProduct);
            await Context.SaveChangesAsync();



            return newProduct.Id;
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
            if (pr == null)
            {
                return NotFound();
            }
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
                return NotFound();
            }
            return pd.NumberOfViewers;
        }

        [Route("InputPurchase/{id_product}")]
        [HttpPut]
        public async Task<ActionResult> InputPurchase(int id_product, [FromBody] Product product)
        {
            Product p = await Context.Products.FindAsync(id_product);
            if (p == null)
            {
                return NotFound();
            }

            p.AddToCart = product.AddToCart;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("InputBuy/{id_user}")]
        [HttpPut]
        public async Task<ActionResult> InputBuy(int id_user, [FromBody] List<Product> products)
        {

            foreach (Product product in products)
            {
                Product p = await Context.Products.FindAsync(product.Id);
                if (p == null)
                {
                    return NotFound();
                }
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
            return Ok();
        }

        [Route("InputProduct/{id_product}")]
        [HttpPut]
        public async Task<ActionResult> InputProduct(int id_product)
        {
            Product p = await Context.Products.FindAsync(id_product);
            if (p == null)
            {
                return NotFound();
            }
            p.AddToCart = false;
            p.Buy = false;

            Context.Products.Update(p);
            await Context.SaveChangesAsync();
            return Ok();
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
        public async Task<ActionResult> RemoveNumberOfView(int id_view)
        {
            NumberOfViewe viewe = await Context.NumberOfViewes.FindAsync(id_view);
            if (viewe == null)
            {
                return NotFound();
            }

            Context.Remove(viewe);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("RemoveNumberOfWish/{id_wish}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveNumberOfWish(int id_wish)
        {
            NumberOfWish wish = await Context.NumberOfWish.FindAsync(id_wish);
            if (wish == null)
            {
                return NotFound();
            }

            Context.Remove(wish);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("RemoveNumberOfLike/{id_like}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveNumberOfLike(int id_like)
        {
            NumberOfLike like = await Context.NumberOfLike.FindAsync(id_like);
            if (like == null)
            {
                return NotFound();
            }
            Context.Remove(like);
            await Context.SaveChangesAsync();
            return Ok();
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