using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiConnection.Entities;
using ApiConnection.Interfaces;
using ePortalApi.Interfaces;
using ePortalApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 

namespace ePortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
      
        [HttpGet("{idUser:int}/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int idUser)
        {
            var data = await _service.GetOneUser(idUser).ConfigureAwait(false);
            return Ok(data);
        }

        [HttpGet("/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetListUsers()
        {
            var users = await _service.GetUsers().ConfigureAwait(false);
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(User user)
        {
            var result = await _service.SaveUser(user).ConfigureAwait(false);
            if (result <= 0) NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(User user)
        {
            var result = await _service.UpdateUser(user).ConfigureAwait(false);
            if (result <= 0) NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUser(id).ConfigureAwait(false);
            if (result <= 0) NotFound();
            return Ok(result);
        }
    }
}