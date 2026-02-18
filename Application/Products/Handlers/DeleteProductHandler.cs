using MediatR;
using NadinSoftTask.Application.Products.Commands;
using NadinSoftTask.Infrastructure.Persistence;

namespace NadinSoftTask.Application.Products.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly AppDbContext _db;

        public DeleteProductHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null) return false; // محصول پیدا نشد

            // فقط کاربری که ساخته اجازه حذف دارد
            if (product.CreatedByUserId != request.UserId)
                throw new UnauthorizedAccessException("You cannot delete this product");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
