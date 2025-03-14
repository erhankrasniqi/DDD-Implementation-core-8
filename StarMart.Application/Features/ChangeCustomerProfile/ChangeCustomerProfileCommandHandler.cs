using MediatR;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.ChangeCustomerProfile
{
    public class ChangeCustomerProfileCommandHandler : IRequestHandler<ChangeCustomerProfileCommand, GeneralResponse<int>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeCustomerProfileCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<int>> Handle(ChangeCustomerProfileCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            Customer customer = await _customerRepository.GetById(request.CustomerId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (customer == null) throw new ApplicationException("Invalid customer.");

            customer.ChangePersonalInfo(request.FirstName, request.LastName);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Customer profile updated successfully.",
                Result = customer.Id
            };
        }
    }
}
