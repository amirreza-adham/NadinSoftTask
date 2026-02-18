using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NadinSoftTask.Domain.Entities;
using NadinSoftTask.DTOs.Products;
using NadinSoftTask.Infrastructure.Persistence;
using System.Security.Claims;

namespace NadinSoftTask.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = _mapper.Map<Product>(dto);
            product.CreatedByUserId = userId;

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? createdByUserId)
        {
            var query = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(createdByUserId))
                query = query.Where(p => p.CreatedByUserId == createdByUserId);

            var products = await query.ToListAsync();
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, CreateProductDto dto)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.CreatedByUserId != userId) return Forbid();

            _mapper.Map(dto, product); 
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }




        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.CreatedByUserId != userId) return Forbid();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok("the product removed");
        }

    }
}
