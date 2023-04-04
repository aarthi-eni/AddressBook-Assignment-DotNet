using Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Entities.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Contracts.IServices;

namespace AddressBookApi.Controllers
{

    [ApiController]
    [Route("api")]
    public class FileController : ControllerBase
    {
        private readonly IAssetService _assetServices;
        private readonly ILogger _logger;

        public FileController(IAssetService assetServices, ILogger logger)
        {
            _assetServices = assetServices ?? throw new ArgumentNullException(nameof(assetServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary> 
        ///Upload User Image
        ///</summary>
        ///<remarks>To upload the user image in db and return image details</remarks> 
        ///<param name="userId"></param> 
        ///<param name="file"></param> 
        ///<response code = "200" >Uploaded image details returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response> 
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpPost("UploadFile")]
        [Route("/addressbook/asset")]
        [SwaggerOperation(Summary ="Image Upload",Description="To upload the user image in db and return image detail")]
        [SwaggerResponse(statusCode: 200, type: typeof(AssetResultDto), description: "Image uploaded successfully")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal server error")]

        public ActionResult UploadImage([FromForm] AssetDto file, Guid AddressBookId)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok( _assetServices.StoreImage( file, userId,AddressBookId));
        }
    }
}