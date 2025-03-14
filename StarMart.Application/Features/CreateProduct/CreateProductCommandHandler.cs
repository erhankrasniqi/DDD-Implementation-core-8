using MediatR;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.Infrastructure.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GeneralResponse<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            Product product = Product.Create(request.Name, request.Price);

            await _productRepository.Add(product, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Product created successfully.",
                Result = product.Id
            };
        }
    }
}
