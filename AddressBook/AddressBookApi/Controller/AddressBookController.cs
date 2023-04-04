using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Entities.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Contracts.IServices;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService _addressBookServices;
        private readonly ILogger _logger;

        public AddressBookController(IAddressBookService userServices, ILogger logger)
        {
            _addressBookServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Create address
        /// </summary>
        /// <remarks>Create address for the user.</remarks>
        /// <param name="body">Created AddressBook object</param>
        /// <response code="201">AddressBook Created Successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict occurred</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpPost("address-book")]
        [SwaggerOperation("CreateAddressBook")]
        [SwaggerResponse(statusCode: 201, type: typeof(string), description: "AddressBook Created Successfully")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Input Model is not valid")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "User is not authorized")]
        [SwaggerResponse(statusCode: 409, type: typeof(ErrorResponse), description: "Conflict Occurred")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal server error")]
        public IActionResult CreateAddressBook(CreateAddressBookDto addressBook)
        {
            Guid UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _logger.LogInformation("Create a addressbook");
            return StatusCode(201, _addressBookServices.CreateNewAddressBookUser(addressBook, UserId));
        }

        /// <summary>
        /// Get all address books
        /// </summary>
        /// <remarks>Get the list of address for the user.</remarks>
        /// <response code="200">Fetched the list of addressbook successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpGet("address-book")]
        [SwaggerOperation("GetAddressBooks")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<AddressBookDto>), description: "Fetched the list of addressbook successfully")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "User is not authorized")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal server error")]
        public IActionResult GetAddressBook()
        {
            //Guid UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _logger.LogInformation("Returned all address book");
            return Ok( _addressBookServices.FetchAddressBookDetail());
        }
        ///<summary> 
        ///Get Address Book Count
        ///</summary>
        ///<remarks>To get the total count of all address book stored in the database</remarks> 
        ///<response code = "200" >count of total address book returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("address-book/count")]
        [SwaggerOperation(Summary = "Get Address Book Count", Description = "To get all the address book stored in the database")]
        [SwaggerResponse(200, "Success", typeof(CountSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]

        public IActionResult GetAddressBookCount()
        {
            int count = _addressBookServices.GetCount().Count();
            _logger.LogInformation("Returned address book count");
            return Ok(new CountSuccessResponse() { count = count });
        }

        /// <summary>
        /// Get addressbook by id
        /// </summary>
        /// <remarks>Getting the addressbook by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">Fetched the addressbook successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpGet("address-book/{id}")]
        [SwaggerOperation("GetAddressBookById")]
        [SwaggerResponse(statusCode: 200, type: typeof(AddressBookDto), description: "Fetched the addressbook successfully")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Input Model is not valid")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "User is not authorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse), description: "AddressBook Id not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal server error")]
        public IActionResult GetAddressBookById(Guid id)
        {
            _logger.LogInformation($"Get addressbook details of id {id}");
            return Ok(_addressBookServices.GetAddressBookById(id));
        }

        /// <summary>
        /// Update AddressBook user by Id
        /// </summary>
        /// <remarks>This api is used to update the addressbook details</remarks>
        /// <param name="id"></param>
        /// <param name="body">Update an existent address book</param>
        /// <response code="200">User updated successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User not found</response>
        /// <response code="409">Conflict occurred</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpPut("address-book/{id}")]
        [SwaggerOperation("UpdateAddressBook")]
        [SwaggerResponse(statusCode: 204, type: typeof(string), description: "AddressBook Created Successfully")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Id is not valid")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "User is not authorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse), description: "AddressBook Id not found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ErrorResponse), description: "Conflict Occurred")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal server error")]
        public IActionResult UpdateAddressBook(Guid id, CreateAddressBookDto addressBook)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _addressBookServices.UpdateAddressBook(id, addressBook, userId);
            _logger.LogInformation($"Update the addressbook details of id {id}");
            return StatusCode(204, "AddressBook Updated");
        }

        /// <summary>
        /// Delete user ById
        /// </summary>
        /// <remarks>This api is used to delete the addressbook record</remarks>
        /// <param name="id"></param>
        /// <response code="204">User deleted successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpDelete("address-book/{id}")]
        [SwaggerOperation(Summary = "Delete Address Book", Description = "To delet an address book from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult DeleteAddressBook(Guid id)
        {
            _addressBookServices.DeleteAddressBook(id);//fetch in service
            _logger.LogInformation("Address book deleted");
            return Ok( "Address book deleted");
        }

    }
}