using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NadinSoftTask.Application.Products.Queries;
using NadinSoftTask.DTOs.Products;
using NadinSoftTask.Infrastructure.Persistence;
namespace NadinSoftTask.Application.Products.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(request.CreatedByUserId))
                query = query.Where(p => p.CreatedByUserId == request.CreatedByUserId);

            var products = await query.ToListAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}

