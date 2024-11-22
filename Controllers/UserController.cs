using Crud.Data;
using Crud.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crud.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private UserContext _userContext;

    public UserController(UserContext userContext)
    {
        _userContext = userContext;
    }
    [HttpPost]
    public IActionResult CreateNewUser([FromBody] UserModel usuario)
    {
        _userContext.Users.Add(usuario);
        _userContext.SaveChanges();

        return Created("Usuario criado", usuario);
    }

    [HttpGet]
    public IEnumerable<UserModel> GetAllUsers([FromQuery] int skip = 0, [FromQuery] int take = 50   )
    {
        return _userContext.Users.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var usuario = _userContext.Users
            .Where(user => user.Id == id)
            .Select(user => new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Ativo
            })
            .FirstOrDefault();

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(usuario);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserModel updatedUser)
    {
        var existingUser = _userContext.Users.FirstOrDefault(user => user.Id == id);

        if (existingUser == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;
        existingUser.Ativo = updatedUser.Ativo;

        _userContext.SaveChanges();

        return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userContext.Users.FirstOrDefault(user => user.Id == id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        _userContext.Users.Remove(user);
        _userContext.SaveChanges();

        return NoContent();
    }


}
