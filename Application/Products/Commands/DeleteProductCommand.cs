using MediatR;

namespace NadinSoftTask.Application.Products.Commands
{
    public record DeleteProductCommand(int Id, string UserId) : IRequest;

}
