using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttribute;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Orders.CompleteOrder;
using ECommerceAPI.Application.Features.Commands.Orders.CreateOrder;
using ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;
using ECommerceAPI.Application.Features.Queries.Orders.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest request)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
