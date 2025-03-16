using Moq;
using StarMart.Application.Features.CreateCustomer;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using Bogus; 

namespace StarMart.Tests.Handlers.Customers
{
    [TestFixture]
    public class CreateCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _mockCustomerRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private CreateCustomerCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _handler = new CreateCustomerCommandHandler(
                _mockCustomerRepo.Object,
                _mockUnitOfWork.Object);   
        }

        [Test]
        public async Task Handle_ShouldCreateCustomerAndReturnSuccess()
        {
            // Arrange 

            var faker = new Faker<CreateCustomerCommand>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
                .RuleFor(c => c.Address, f => f.Address.StreetAddress());

            var command = faker.Generate();


            _mockCustomerRepo
                .Setup(r => r.Add(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Customer created successfully."));
            _mockCustomerRepo.Verify(r => r.Add(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
