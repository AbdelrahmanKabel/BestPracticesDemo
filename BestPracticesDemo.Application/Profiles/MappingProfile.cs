using AutoMapper;
using BestPracticesDemo.Application.Features.Categorys.Commands.CreateCategory;
using BestPracticesDemo.Application.Features.Categorys.Queries.GetCategoriesList;
using BestPracticesDemo.Application.Features.Categorys.Queries.GetCategoriesListWithEvents;
using BestPracticesDemo.Application.Features.Events.Commands.CreateEvent;
using BestPracticesDemo.Application.Features.Events.Commands.UpdateEvent;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventDetails;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsList;
using BestPracticesDemo.Domain.Entities;

namespace BestPracticesDemo.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailsVm>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            //CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();


        }
    }
}
