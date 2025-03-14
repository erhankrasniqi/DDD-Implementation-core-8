using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarMart.Application.Features.CreateProduct;
using StarMart.Application.Features.ProductsList;
using StarMart.Application.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarMart.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : DefaultController
    {
        public ProductController(IMediator mediator)
            : base(mediator)
        {
            //
        }

        [HttpGet]
        public async Task<GeneralResponse<IEnumerable<ProductReadModel>>> GetProducts()
        {
            return await Mediator.Send(new ProductsListQuery()).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<GeneralResponse<int>> PostCreate([FromBody] CreateProductCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }
    }
}
