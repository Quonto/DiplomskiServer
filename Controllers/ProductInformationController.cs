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
    public class ProductInformationController : ControllerBase
    {

        public CategoryContext Context { get; set; }

        public ProductInformationController(CategoryContext context)
        {
            Context = context;
        }

        [Route("InputProductInformation/{id_group}")]
        [HttpPost]
        public async Task<ActionResult<int>> InputProductInformation(int id_group, [FromBody] ProductInformation productInformation)
        {
            Group gr = await Context.Groups.FindAsync(id_group);
            if (gr == null)
            {
                return NotFound();
            }

            productInformation.Groups = gr;

            if (productInformation.Name == "")
            {
                return BadRequest("Niste uneli naziv informacije o proizvodu");
            }

            ProductInformation pi = await Context.ProductInformation.Where(pi => pi.Name == productInformation.Name && pi.Groups.Id == gr.Id).FirstOrDefaultAsync();

            if (pi != null)
            {
                pi.Delete = false;
                Context.ProductInformation.Update(pi);
                await Context.SaveChangesAsync();
                return pi.Id;
            }

            ProductInformation curentPI = new ProductInformation();

            curentPI.Delete = false;
            curentPI.Groups = productInformation.Groups;
            curentPI.Name = productInformation.Name;


            Context.ProductInformation.Add(curentPI);
            await Context.SaveChangesAsync();

            return curentPI.Id;
        }

        [Route("FetchProductInformation/{id_group}")]
        [HttpGet]
        public async Task<ActionResult<List<ProductInformation>>> FetchProductInformation(int id_group)
        {
            List<ProductInformation> pr = await Context.ProductInformation.Where(g => g.Groups.Id == id_group && g.Delete == false).ToListAsync();
            return pr;
        }

        [Route("UpdateProductInformation")]
        [HttpPut]
        public async Task<ActionResult> UpdateProductInformation([FromBody] ProductInformation productInformation)
        {
            if (productInformation.Name == "")
            {
                return BadRequest("Niste uneli naziv informacije o proizvodu");
            }
            Context.ProductInformation.Update(productInformation);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("RemoveProductInformation/{id_product_information}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveProductInformation(int id_product_information)
        {
            ProductInformation pi = await Context.ProductInformation.FindAsync(id_product_information);
            if (pi == null)
            {
                return NotFound();
            }
            pi.Delete = true;
            Context.ProductInformation.Update(pi);
            await Context.SaveChangesAsync();
            return Ok();
        }


        [Route("RemoveProductInformationData/{id_product_information_data}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveProductInformaionData(int id_product_information_data)
        {
            ProductInformationData pid = await Context.ProductInformationData.FindAsync(id_product_information_data);
            if (pid == null)
            {
                return NotFound();
            }
            Context.Remove(pid);
            await Context.SaveChangesAsync();
            return Ok();
        }

    }
}