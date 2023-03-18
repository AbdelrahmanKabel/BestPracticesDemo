using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Application.Features.Categorys.Queries.GetCategoriesList;
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

namespace BestPracticesDemo.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetCategoriesListQueryHandler ( _mockCategoryRepository.Object, _mapper );
            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<CategoryListVm>>();
            result.Count.ShouldBe(4);
        }
    }
}
