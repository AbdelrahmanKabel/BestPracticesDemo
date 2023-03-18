using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Application.Features.Categorys.Commands.CreateCategory;
using BestPracticesDemo.Application.Profiles;
using BestPracticesDemo.Application.UnitTests.Mocks;
using BestPracticesDemo.Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BestPracticesDemo.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task CreateCategoryTest()
        {
            var handler = new CreateCategoryCommandHandler(_mockCategoryRepository.Object, _mapper);
            await handler.Handle(new CreateCategoryCommand() { Name = "New Category"}, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.GetAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}
