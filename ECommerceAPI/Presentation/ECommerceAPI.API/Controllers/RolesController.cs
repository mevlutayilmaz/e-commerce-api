using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttribute;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.AppRole.CreateRole;
using ECommerceAPI.Application.Features.Commands.AppRole.DeleteRole;
using ECommerceAPI.Application.Features.Commands.AppRole.UpdateRole;
using ECommerceAPI.Application.Features.Queries.AppRole.GetAllRoles;
using ECommerceAPI.Application.Features.Queries.AppRole.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get All Roles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesQueryRequest request)
        {
            GetAllRolesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Role By Id")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest request)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
        {
            CreateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut()]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Updating, Definition = "Update Role")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommandRequest request)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Deleting, Definition = "Delete Role")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest request)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
