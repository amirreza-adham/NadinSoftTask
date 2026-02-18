using AutoMapper;
using MediatR;
using NadinSoftTask.Application.Products.Commands;
using NadinSoftTask.Domain.Entities;
using NadinSoftTask.DTOs.Products;
using NadinSoftTask.Infrastructure.Persistence;


namespace NadinSoftTask.Application.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CreateProductHandler(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Dto);
            product.CreatedByUserId = request.UserId;

            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
