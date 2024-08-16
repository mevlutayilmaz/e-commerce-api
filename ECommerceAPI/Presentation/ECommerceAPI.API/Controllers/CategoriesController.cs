using ECommerceAPI.Application.Features.Commands.Categories.CreateCategory;
using ECommerceAPI.Application.Features.Queries.Categories.GetAllCategories;
using ECommerceAPI.Application.Features.Queries.Categories.GetCategory;
using ECommerceAPI.Application.Features.Queries.Categories.GetRootCategories;
using ECommerceAPI.Application.Features.Queries.Categories.GetSubCategories;
using ECommerceAPI.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {
            CreateCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQueryRequest request)
        {
            GetAllCategoriesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRootCategories([FromQuery]GetRootCategoriesQueryRequest request)
        {
            GetRootCategoriesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{ParentId}")]
        public async Task<IActionResult> GetSubCategories([FromRoute] GetSubCategoriesQueryRequest request)
        {
            GetSubCategoriesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> GetCategory([FromRoute] GetCategoryQueryRequest request)
        {
            GetCategoryQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
