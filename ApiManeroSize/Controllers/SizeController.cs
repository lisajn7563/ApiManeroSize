using ApiManeroSize.Contexts;
using ApiManeroSize.Entites;
using ApiManeroSize.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroSize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost]

        public async Task<IActionResult> Create(SizeRegistration model)
        {
            if (ModelState.IsValid)
            {
                var entity = new SizeEntity
                {
                    Id = model.id,
                    sizeTitle = model.SizeTitle
                };
                _context.Size.Add(entity);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sizeList = await _context.Size.ToListAsync();

            return Ok(sizeList);
        }
    }
}
