
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        IQueryable<User> users = userRepo.GetManyAsync();

        bool usernameExists = users.Any(u => u.Username == request.UserName);

        if (usernameExists)
        {
            return BadRequest("Username already exists");
        }

        User user = new()
        {
            Username = request.UserName,
            Password = request.Password
        };

        User created = await userRepo.AddAsync(user);

        UserDto dto = new()
        {
            Id = created.Id,
            Username = created.Username
        };

        return Created($"/users/{dto.Id}", dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);

            UserDto dto = new()
            {
                Id = user.Id,
                Username = user.Username
            };

            return Ok(dto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers(
        [FromQuery] string? usernameContains = null)
    {
        IQueryable<User> users = userRepo.GetManyAsync();

        if (usernameContains != null)
        {
            users = users.Where(u => u.Username.Contains(usernameContains));
        }

        List<UserDto> dtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username
        }).ToList();

        return Ok(dtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] int id,
        [FromBody] UpdateUserDto request)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);

            IQueryable<User> users = userRepo.GetManyAsync();

            bool usernameExists = users.Any(u =>
                u.Username == request.Username && u.Id != id);

            if (usernameExists)
            {
                return BadRequest("Username already exists");
            }

            user.Username = request.Username;
            user.Password = request.Password;

            await userRepo.UpdateAsync(user);

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
