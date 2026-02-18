using MediatR;
using NadinSoftTask.DTOs.Products;

namespace NadinSoftTask.Application.Products.Queries
{
    public record GetAllProductsQuery(string? CreatedByUserId) : IRequest<List<ProductDto>>;
}
