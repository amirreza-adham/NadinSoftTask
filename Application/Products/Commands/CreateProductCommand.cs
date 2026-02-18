using MediatR;
using NadinSoftTask.DTOs.Products;

namespace NadinSoftTask.Application.Products.Commands
{
    public record CreateProductCommand(CreateProductDto Dto, string UserId) : IRequest<ProductDto>;
}
