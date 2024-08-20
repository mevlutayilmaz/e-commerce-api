using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEnpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesToEndpoint([FromQuery] GetRolesToEnpointQueryRequest request)
        {
            GetRolesToEnpointQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
