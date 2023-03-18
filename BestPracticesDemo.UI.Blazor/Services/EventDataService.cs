using AutoMapper;
using BestPracticesDemo.UI.Blazor.Contracts;
using BestPracticesDemo.UI.Blazor.Services.Base;
using BestPracticesDemo.UI.Blazor.ViewModels;
using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Services
{
    public class EventDataService : BaseDataService, IEventDataService
    {

        private readonly IMapper _mapper;

        public EventDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<List<EventListViewModel>> GetAllEvents()
        {
            await AddBearerToken();

            var allEvents = await _client.GetAllEventsAsync();
            var mappedEvents = _mapper.Map<ICollection<EventListViewModel>>(allEvents);
            return mappedEvents.ToList();
        }

        public async Task<EventDetailViewModel> GetEventById(Guid id)
        {
            await AddBearerToken();

            var selectedEvent = await _client.GetEventByIdAsync(id);
            var mappedEvent = _mapper.Map<EventDetailViewModel>(selectedEvent);
            return mappedEvent;
        }

        public async Task<ApiResponse<Guid>> CreateEvent(EventDetailViewModel eventDetailViewModel)
        {
            try
            {
                await AddBearerToken();

                CreateEventCommand createEventCommand = _mapper.Map<CreateEventCommand>(eventDetailViewModel);
                var newId = await _client.AddEventAsync(createEventCommand);
                return new ApiResponse<Guid>() { Data = newId, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> UpdateEvent(EventDetailViewModel eventDetailViewModel)
        {
            try
            {
                await AddBearerToken();

                UpdateEventCommand updateEventCommand = _mapper.Map<UpdateEventCommand>(eventDetailViewModel);
                await _client.UpdateEventAsync(updateEventCommand);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> DeleteEvent(Guid id)
        {
            try
            {
                await AddBearerToken();

                await _client.DeleteEventAsync(id);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}