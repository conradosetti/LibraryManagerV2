using LibraryManager.Application.Models;
using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Services;
using LibraryManager.Application.Users.Commands.Create;
using LibraryManager.Application.Users.Queries.Get;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class UserController(IUserService service, IMediator mediator) : ControllerBase
{
   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetById(int id)
   {
      //var result = await service.GetUserByIdAsync(id);
      var result = await mediator.Send(new GetUserQuery(id));
      if (!result.IsSuccess)
         return NotFound(result.Message);
      return Ok(result);
   }
   
   [HttpPost]
   public async Task<IActionResult> Create(CreateUserCommand command)
   {
      //var result = await service.CreateUserAsync(model);
      var result = await mediator.Send(command);
      return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
   }
}