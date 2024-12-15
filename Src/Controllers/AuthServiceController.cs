using Microsoft.AspNetCore.Mvc;
using ApiGateway.DTOs; 
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthServiceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _authServiceUrl = "https://localhost:5095/api/auth";

        public AuthServiceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Ruta para login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/login", loginRequestDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(new { Token = result }); 
            }

            return Unauthorized();  
        }

        // Ruta para registro
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto registerStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/register", registerStudentDto);
            if (response.IsSuccessStatusCode)
            {
                return CreatedAtAction(nameof(Login), new { email = registerStudentDto.Email }, registerStudentDto);
            }

            return BadRequest("Registration failed");
        }

        // Ruta para actualizar contraseña
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _httpClient.PutAsJsonAsync($"{_authServiceUrl}/update-password", updatePasswordDto);
            if (response.IsSuccessStatusCode)
            {
                return NoContent(); 
            }

            return BadRequest("Password update failed");
        }
    }
}
