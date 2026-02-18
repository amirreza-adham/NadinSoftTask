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

    }
}
