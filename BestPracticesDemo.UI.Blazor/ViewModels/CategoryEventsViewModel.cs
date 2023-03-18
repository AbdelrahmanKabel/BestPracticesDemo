using System;
using System.Collections.Generic;

namespace BestPracticesDemo.UI.Blazor.ViewModels
{
    public class CategoryEventsViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<EventNestedViewModel> Events { get; set; }
    }
}
