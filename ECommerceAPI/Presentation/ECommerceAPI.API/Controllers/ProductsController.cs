using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttribute;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.CreateProducts;
using ECommerceAPI.Application.Features.Commands.Products.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetProductById;
using ECommerceAPI.Application.Features.Queries.Products.SearchProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest request)
        {

            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProduct([FromQuery] SearchProductQueryRequest request)
        {
            SearchProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] GetProductByIdQueryRequest request)
        {
            GetProductByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProducts(CreateProductsCommandRequest request)
        {
            CreateProductsCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> UpdateProduct (UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Remove Product")]
        public async Task<IActionResult> RemoveProduct([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

    }
}
