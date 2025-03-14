using MediatR;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.ProductsList
{
    public class ProductsListQueryHandler : IRequestHandler<ProductsListQuery, GeneralResponse<IEnumerable<ProductReadModel>>>
    {
        private readonly IProductRepository _productRepository;

        public ProductsListQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GeneralResponse<IEnumerable<ProductReadModel>>> Handle(ProductsListQuery query, CancellationToken cancellationToken = default)
        {
            IEnumerable<Product> products = await _productRepository.Get(cancellationToken: cancellationToken).ConfigureAwait(false);
            IEnumerable<ProductReadModel> readModel = [];

            if (products.Any())
            {
                readModel = products.Select(x =>
                new ProductReadModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                });
            }

            return new GeneralResponse<IEnumerable<ProductReadModel>>
            {
                Success = true,
                Message = "Products list.",
                Result = readModel
            };
        }
    }
}
