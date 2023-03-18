using BestPracticesDemo.Application.Features.Categorys.Commands.CreateCategory;
using BestPracticesDemo.Application.Features.Categorys.Queries.GetCategoriesList;
using BestPracticesDemo.Application.Features.Categorys.Queries.GetCategoriesListWithEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesListQuery());

            return Ok(categories);
        }

        [HttpGet("GetCategoriesWithEvents")]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includePastEvents)
        {
            GetCategoriesListWithEventsQuery getCategoriesListWithEventsQuery = new GetCategoriesListWithEventsQuery { includePastEvents = includePastEvents }; 
            var categories = await _mediator.Send(getCategoriesListWithEventsQuery);

            return Ok(categories);
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);

            return Ok(response);
        }
    }
}
