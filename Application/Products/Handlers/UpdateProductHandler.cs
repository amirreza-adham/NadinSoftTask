using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NadinSoftTask.Application.Products.Commands;
using NadinSoftTask.DTOs.Products;
using NadinSoftTask.Infrastructure.Persistence;

namespace NadinSoftTask.Application.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UpdateProductHandler(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null) throw new KeyNotFoundException("Product not found");

            if (product.CreatedByUserId != request.UserId) throw new UnauthorizedAccessException();

            _mapper.Map(request.Dto, product);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }

}
