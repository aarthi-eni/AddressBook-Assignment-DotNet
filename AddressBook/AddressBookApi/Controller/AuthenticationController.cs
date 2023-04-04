using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Entities.Dtos;
using Contracts.IServices;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IAuthServices _userService;

        public UserController(IAuthServices userService)
        {
            _userService = userService;
        }

        ///<summary> 
        ///To login user
        ///</summary>
        ///<remarks>To create and return session for valid user</remarks> 
        ///<param name="login"></param> 
        ///<response code = "200" >Session type and token returned succesfully</response> 
        ///<response code = "403" >Password wrong</response> 
        ///<response code = "404" >User email not found</response>
        ///<response code = "400" >User email not valid</response> 
        ///<response code="500">Internel server error</response>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login User", Description = "Login user and return session token")]
        [SwaggerResponse(200, "Success", typeof(TokenDto))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorDto))]
        [SwaggerResponse(400, "BadRequest", typeof(ErrorDto))]
        [SwaggerResponse(404, "NotFound", typeof(ErrorDto))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorDto))]
        public ActionResult LoginUser([FromBody] UserDto loginCredentials)
        {
            return Ok(_userService.ValidateUserInputLogin(loginCredentials));
        }
    }
}   
