using BestPracticesDemo.UI.Blazor.Services.Base;
using BestPracticesDemo.UI.Blazor.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Contracts
{
    public interface IEventDataService
    {
        Task<List<EventListViewModel>> GetAllEvents();
        Task<EventDetailViewModel> GetEventById(Guid id);
        Task<ApiResponse<Guid>> CreateEvent(EventDetailViewModel eventDetailViewModel);
        Task<ApiResponse<Guid>> UpdateEvent(EventDetailViewModel eventDetailViewModel);
        Task<ApiResponse<Guid>> DeleteEvent(Guid id);
    }
}
