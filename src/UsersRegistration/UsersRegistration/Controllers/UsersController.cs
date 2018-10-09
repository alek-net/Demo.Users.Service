using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace UsersRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private readonly IConfiguration _configuration;

		public UsersController(IConfiguration configuration)
		{
			_configuration = configuration;			
		}

		// GET api/users
		[HttpGet]
        public async Task<IActionResult> Get(int tenantId)
        {
			var service = ServiceFactory.CreateUsersService(
				_configuration.GetConnectionString("UsersDatabase"));

			var users = await service.GetUsers(tenantId);

			return Ok(users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int tenantId, int id)
        {
			var service = ServiceFactory.CreateUsersService(
				_configuration.GetConnectionString("UsersDatabase"));

			var user = await service.GetUserById(id);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] CreateUserRequest request)
        {
			var service = ServiceFactory.CreateUsersService(
				_configuration.GetConnectionString("UsersDatabase"));

			var response = await service.CreateUser(request);

			if (response.Status == ResponseStatus.Success)
			{
				return Accepted(response.User);
			}
			else if (response.Status == ResponseStatus.ValidationError)
			{
				return BadRequest(response.Message);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			
		}

        // PUT api/users/5
        [HttpPut]
        public void Put(int id, [FromBody] UpdateUserRequest request)
        {
			var service = ServiceFactory.CreateUsersService(
				_configuration.GetConnectionString("UsersDatabase"));

			service.UpdateUser(request);
		}		

		// DELETE api/users/5
		[HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
			var service = ServiceFactory.CreateUsersService(
				_configuration.GetConnectionString("UsersDatabase"));
			
			var result = await service.DeleteUser(request);

			if (result)
			{
				return Ok();
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

		}
    }
}
