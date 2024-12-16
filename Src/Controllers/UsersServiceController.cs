using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Grpc;

namespace ApiGateway.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersServiceController : ControllerBase
    {
        private readonly UserService.UserServiceClient _userServiceClient;

        public UsersServiceController(UserService.UserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditProfileRequest request)
        {
            var response = await _userServiceClient.EditProfileAsync(request);
            return Ok(response);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var response = await _userServiceClient.GetProfileAsync(new EmptyRequest());
            return Ok(response);
        }

        [HttpGet("my-progress")]
        public async Task<IActionResult> GetMyProgress()
        {
            var response = await _userServiceClient.GetUserProgressAsync(new EmptyRequest());
            return Ok(response.Progress);
        }

        [HttpPatch("my-progress")]
        public async Task<IActionResult> UpdateMyProgress([FromBody] UpdateUserProgressRequest request)
        {
            await _userServiceClient.SetUserProgressAsync(request);
            return Ok();
        }
    }
}
