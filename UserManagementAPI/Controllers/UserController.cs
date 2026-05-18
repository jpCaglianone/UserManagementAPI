using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            return Ok(_us.Users);

        }
  
        [HttpPost]
        public IActionResult insertUser(string name)
        {
            bool insert = _us.insertUser(name);
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
        public IActionResult deleteUser(string name)
        {
            if (_us.deleteUser(name))
            {
                return Ok("User deleted successfully");
            }
            return StatusCode(500, "Error user not found");
        }

        [HttpPut]
        public IActionResult putUser(string name, string newName)
        {
            if (!_us.updateUser(name, newName))
            {
                return StatusCode(500, "Error user not found");
            }
            return Ok("User updated successfully");
        }
    }
}