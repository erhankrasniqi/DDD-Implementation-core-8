using MediatR;
using StarMart.Application.Responses;

namespace StarMart.Application.Features.CreateProduct
{
    public class CreateProductCommand : IRequest<GeneralResponse<int>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
