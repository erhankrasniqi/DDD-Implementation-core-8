using Moq;
using StarMart.Application.Features.CreateProduct;
using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.Infrastructure.Contracts;
using Bogus;

namespace StarMart.Tests.Handlers.Products
{
    [TestFixture]
    public class CreateProductCommandHandlerTests
    {
        private Mock<IProductRepository> _mockProductRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private CreateProductCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _handler = new CreateProductCommandHandler(
                _mockProductRepo.Object,
                _mockUnitOfWork.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateProductAndReturnSuccess()
        {
            // Arrange 

            var faker = new Faker<CreateProductCommand>()
                .RuleFor(c => c.Name, f => f.Commerce.ProductName())   
                .RuleFor(c => c.Price, f => Convert.ToDecimal(f.Commerce.Price())); 

            var command = faker.Generate();


            _mockProductRepo
                .Setup(r => r.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);   


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Product created successfully."));   
            _mockProductRepo.Verify(r => r.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
