using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsList;
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

namespace BestPracticesDemo.Application.UnitTests.Events.Queries
{
    public class GetEventsListQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Event>> _mockEventRepository;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandlerTests()
        {
            _mockEventRepository = RepositoryMocks.GetEventsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();

        }

        [Fact]
        public async Task GetEventsListTest()
        {
            var handler = new GetEventListQueryHandler(_mockEventRepository.Object, _mapper);
            var  result = await handler.Handle(new GetEventListQuery(), CancellationToken.None);
            result.ShouldBeOfType(typeof(List<EventListVm>));
            result.Count.ShouldBe(6);
        }
    }
}
