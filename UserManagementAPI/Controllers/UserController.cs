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

            return Ok(_us.SelectUsers());

        }
  
        [HttpPost]
        public IActionResult insertUser(string name)
        {
            if (string.IsNullOrEmpty(name)) 
            {
                return BadRequest("Name cannot be null or empty");
            }

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
        public IActionResult deleteUser(int id)
        {
            if (_us.deleteUser(id))
            {
                return Ok("User deleted successfully");
            }
            return StatusCode(500, "Error user not found");
        }

        [HttpPut]
        public IActionResult putUser(int id, string newName)
        {
            if (!_us.updateUser(id, newName))
            {
                return StatusCode(500, "Error user not found");
            }
            return Ok("User updated successfully");
        }
    }
}