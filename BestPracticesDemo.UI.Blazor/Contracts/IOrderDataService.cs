using BestPracticesDemo.UI.Blazor.ViewModels;
using System;
using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Contracts
{
    public interface IOrderDataService
    {
        Task<PagedOrderForMonthViewModel> GetPagedOrderForMonth(DateTime date, int page, int size);
    }
}
