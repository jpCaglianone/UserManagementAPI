using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserManagementAPI.Model.DTO.UserDTO;
using UserManagementAPI.Services;


namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _us;
        public UserController(UserService us)
        {
            _us = us;
        }

        [HttpGet]
        public IActionResult selectOrderId()
        {
            var result = _us.SelectUsers();
            if (result.Count == 0)
            {
                return Ok("Not users in database!");
            }
            return Ok(_us.SelectUsers());

        }
  
        [HttpPost]
        public IActionResult insertUser([FromBody] UserDTO user)
        {


            if (string.IsNullOrEmpty(user.name)) 
            {
                return BadRequest("Name cannot be null or empty");
            }

            bool insert = _us.insertUser(user.name);
            if (insert)
            {
                return Ok("User inserted successfully");
            }
            else
            {
                return StatusCode(500, "Error inserting user");
            }
        }

        [HttpDelete]
        public IActionResult deleteUser([FromBody] UserDTO user)
        {
            if (_us.deleteUser(user.id))
            {
                return Ok("User deleted successfully");
            }
            return StatusCode(500, "Error user not found");
        }

        [HttpPut]
        public IActionResult putUser([FromBody] UserDTO user)
        {
            if (!_us.updateUser(user.id, user.name))
            {
                return StatusCode(500, "Error user not found");
            }
            return Ok("User updated successfully");
        }
    }
}