using MediatR;
using NadinSoftTask.DTOs.Products;


namespace NadinSoftTask.Application.Products.Commands
{
    public record UpdateProductCommand(int Id, CreateProductDto Dto, string UserId) : IRequest<ProductDto>;

}
