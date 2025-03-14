using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarMart.Application.Features.ChangeCustomerProfile;
using StarMart.Application.Features.CreateCustomer;
using StarMart.Application.Features.CustomerOrdersList;
using StarMart.Application.Features.CustomersList;
using StarMart.Application.Features.OrdersListByByDate;
using StarMart.Application.Features.SubmitOrder;
using StarMart.Application.ReadModels;
using StarMart.Application.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarMart.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : DefaultController
    {

        public CustomerController(IMediator mediator)
            : base(mediator)
        {
            //
        }

        [HttpGet]
        public async Task<GeneralResponse<IEnumerable<CustomerReadModel>>> GetCustomers()
        {
            return await Mediator.Send(new CustomersListQuery()).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<GeneralResponse<CustomerOrderReadModel>> GetCustomerOrders([FromQuery] CustomerOrdersListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("orders-by-date")]
        public async Task<GeneralResponse<IEnumerable<OrderReadModel>>> GetCustomerOrdersByDate([FromQuery] OrdersListByDateQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<GeneralResponse<int>> PostCreate([FromBody] CreateCustomerCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("submit-order")]
        public async Task<GeneralResponse<int>> PostCreateOrder([FromBody] SubmitOrderCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("change-profile")]
        public async Task<GeneralResponse<int>> PutChangeProfile([FromBody] ChangeCustomerProfileCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }
    }
}
