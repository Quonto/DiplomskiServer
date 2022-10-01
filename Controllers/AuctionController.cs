using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Novi.Models;
using Novi.Hubs;
using Microsoft.AspNetCore.SignalR;


namespace Novi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : ControllerBase
    {

        public CategoryContext Context { get; set; }
        public IHubContext<ChatHub, IHubClient> HubContext { get; set; }

        public AuctionController(CategoryContext context, IHubContext<ChatHub, IHubClient> hubContext)
        {
            Context = context;
            HubContext = hubContext;
        }

        [Route("InputAuction/{id_product}")]
        [HttpPost]
        public async Task<ActionResult> InputAuction(int id_product, [FromBody] Auction auction)
        {
            Auction newAuction = new Auction();

            if (auction == null)
            {
                return NotFound();
            }

            newAuction.Product = id_product;
            newAuction.MinimumPrice = auction.MinimumPrice;
            newAuction.Time = auction.Time;
            newAuction.User = auction.User;

            Context.Auction.Add(newAuction);
            await Context.SaveChangesAsync();

            return Created("New auction", newAuction);
        }


        [Route("FetchAuction/{id_product}")]
        [HttpGet]
        public async Task<ActionResult<Auction>> FetchAuction(int id_product)
        {

            Auction au = await Context.Auction.Where(a => a.Product == id_product).Select(a => new Auction { Id = a.Id, MinimumPrice = a.MinimumPrice, Product = a.Product, Time = a.Time, User = (a.User == null) ? null : new User { ID = a.User.ID, IsAdmin = a.User.IsAdmin, Picture = a.User.Picture, Username = a.User.Username, UserInformation = null } }).FirstAsync();
            if (au == null)
            {
                return NotFound();
            }

            return au;
        }


        [Route("UpdateAuction/{id_user}")]
        [HttpPut]
        public async Task<ActionResult> UpdateAuction(int id_user, [FromBody] Product product)
        {
            Product p = await Context.Products.Where(p => p.Id == product.Id).Include(p => p.Data).Include(p => p.Data).ThenInclude(pi => pi.ProductInformation).Include(p => p.Place).Include(u => u.User).Include(u => u.User).ThenInclude(u => u.UserInformation).Include(u => u.User).ThenInclude(ui => ui.UserInformation).ThenInclude(pl => pl.Place).Include(r => r.Reviews).Include(r => r.Reviews).ThenInclude(u => u.User).Include(p => p.Picture).Include(n => n.NumberOfViewers).Include(w => w.NumberOfWish).Include(l => l.NumberOfLike).AsSplitQuery().FirstAsync();


            User u = await Context.Users.FindAsync(id_user);


            Auction au = await Context.Auction.Where(a => a.Product == product.Id).FirstAsync();

            if ((au.MinimumPrice + p.Price) > product.Price)
            {
                return BadRequest(
                    "Uneli ste cenu manju od minimalne"
                );
            }

            var currentDate = DateTime.Now;

            if (au.Time.Subtract(currentDate).TotalMinutes < 5.0)
            {
                au.Time = au.Time.AddMinutes(10.0);
            }


            au.User = u;
            p.Price = product.Price;

            Context.Products.Update(p);
            Context.Auction.Update(au);
            await Context.SaveChangesAsync();

            au.User.Password = null;
            au.User.Email = null;
            au.User.Picture = null;


            await HubContext.Clients.All.BroadcastMessage(p, au);


            return Ok();
        }

        [Route("UpdateAuction")]
        [HttpPut]
        public async Task<ActionResult<Auction>> UpdateAuction([FromBody] Auction auction)
        {
            Auction au = await Context.Auction.Where(au => au.Id == auction.Id).Include(a => a.User).FirstAsync();

            if (au == null)
            {
                return NotFound();
            }

            au.Time = auction.Time;
            au.MinimumPrice = auction.MinimumPrice;
            au.User = auction.User;


            Context.Auction.Update(au);
            await Context.SaveChangesAsync();

            return au;
        }


        [Route("UpdateAuctionProduct")]
        [HttpPut]
        public async Task<ActionResult<System.TimeSpan>> UpdateAuctionProduct()
        {
            List<Product> products = await Context.Products.Where(p => p.Auction == true).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }

            var currentDate = DateTime.Now;
            var leftTime = System.TimeSpan.Zero;

            foreach (Product product in products)
            {
                Auction auction = await Context.Auction.Where(a => a.Product == product.Id).FirstAsync();

                if (auction == null)
                {
                    return NotFound();
                }


                leftTime = auction.Time.Subtract(currentDate);
                if (leftTime.TotalSeconds < 0)
                {
                    product.Buy = true;
                    Context.Products.Update(product);
                    await Context.SaveChangesAsync();
                }
            }

            return leftTime;
        }

    }
}
