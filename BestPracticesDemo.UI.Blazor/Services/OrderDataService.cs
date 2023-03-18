using AutoMapper;
using BestPracticesDemo.UI.Blazor.Contracts;
using BestPracticesDemo.UI.Blazor.Services.Base;
using BestPracticesDemo.UI.Blazor.ViewModels;
using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Services
{
    public class OrderDataService : BaseDataService, IOrderDataService
    {
        private readonly IMapper _mapper;

        public OrderDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<PagedOrderForMonthViewModel> GetPagedOrderForMonth(DateTime date, int page, int size)
        {
            var orders = await _client.GetPagedOrdersForMonthAsync(date, page, size);
            var mappedOrders = _mapper.Map<PagedOrderForMonthViewModel>(orders);
            return mappedOrders;
        }
    }
}