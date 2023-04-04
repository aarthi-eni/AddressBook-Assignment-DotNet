using System.Collections.Generic;
using System;
using AutoMapper;
using AddressBookApi.Controllers;
using Xunit;
using Microsoft.Extensions.Logging;
using Entities;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Services;
using Repositories;
using Contracts.IRepositories;
using Contracts.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using CustomExceptionHandling;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace AddressBookUnitTest
{
    public class AddressBookTesting
    {
        private readonly UserController _authController;
        private readonly AddressBookController _addressBookController;
        private readonly AddressBookRepositories _addressBookRepositories;
        private readonly AuthenticationRepositories _authenticationRepositories;
        private readonly IAddressBookService _addressBookServices;
        private readonly IAuthServices _authService;
        private readonly AddressBookContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public   AddressBookTesting()
        {
                     _configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            using ServiceProvider services = new ServiceCollection()
                            .AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(_configuration)
                            // -> add your DI needs here
                            .BuildServiceProvider();

            AddressBookContext context = TestDbContext.addressBookDbContext();
            _context = ContextData.AddData(context);
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder().
           ConfigureLogging((builderContext, loggingBuilder) =>
           {
               loggingBuilder.AddConsole((options) =>
               {
                   options.IncludeScopes = true;
               });
           });
            IHost host = hostBuilder.Build();
            _logger = host.Services.GetRequiredService<ILogger<UserController>>();

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Entities.MappingProfile.Mapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _addressBookRepositories = new AddressBookRepositories(_context);
            _authenticationRepositories = new AuthenticationRepositories(_context);
            _authService = new AuthServices(_configuration,_authenticationRepositories);
            _addressBookServices = new AddressBookService(_mapper, _addressBookRepositories, _logger);
            _addressBookController = new AddressBookController(_addressBookServices, _logger);
            _authController = new UserController(_authService);

            string userId = "db8480d2-c2e3-47ac-8f54-88af14bf35a9";
            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,userId)
                                        // other required and custom claims
                           }, "TestAuthentication")));
            _addressBookController.ControllerContext.HttpContext = contextMock.Object;
            //_fileController.ControllerContext.HttpContext = contextMock.Object;
        }

         /// <summary>
        ///   To test address book count 
        /// </summary>
        [Fact]
        public void GetAddressBookCount_OkObjectResult()
        {
            OkObjectResult response = _addressBookController.GetAddressBookCount() as OkObjectResult;
            Assert.IsType<CountSuccessResponse>(response.Value);

            CountSuccessResponse resultCount = response.Value as CountSuccessResponse;
            Assert.Equal(1, resultCount.count);
        }

        /// <summary>
        ///   To test get address book by user id
        /// </summary>
        [Fact]
        public void GetAddressBookById_OkObjectResult()
        {
            Guid AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a");
            ActionResult response1 = _addressBookController.GetAddressBookById(AddressBookId) as ActionResult;
            Assert.IsType<OkObjectResult>(response1);
            OkObjectResult item = response1 as OkObjectResult;
            Assert.IsType<AddressBookDto>(item.Value);
        }

         /// <summary>
        ///   To test get address book using non existing user id
        /// </summary>
        [Fact]
        public void GetAddressBookById_NotFoundObjectResult()
        {
            Guid AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc016");
              Action response = () => _addressBookController.GetAddressBookById(AddressBookId);
             ExceptionModel exception = Record.Exception(response) as ExceptionModel;
            Assert.Equal(404, exception.error.StatusCode); 
        }

        /// <summary>
        ///  To test the get all address book
        /// </summary>
        [Fact]
        public void GetAllAddress_OkObjectResult()
        {
            ActionResult response1 = _addressBookController.GetAddressBook() as ActionResult;
            Assert.IsType<OkObjectResult>(response1);
            OkObjectResult item = response1 as OkObjectResult;
            Assert.IsType<List<AddressBookDto>>(item.Value);
        }

        /// <summary>
        ///  To test the login user
        /// </summary>
        [Fact]
        public void login_OkObjectResult()
        {
            UserDto loginDetails = new UserDto() { UserName = "Aarthi", Password = "Aarthir@01" };
            IActionResult response1 = _authController.LoginUser(loginDetails);
            Assert.IsType<OkObjectResult>(response1);
        }

         /// <summary>
        ///  To test the login user by invalid data
        /// </summary>
        [Fact]
        public void login_UnauthorizedObjectResult()
        {
            UserDto loginDetails2 = new UserDto() { UserName = "eniyavan", Password = "12345eniyavan" };
             Action response = () => _authController.LoginUser(loginDetails2);
             ExceptionModel exception = Record.Exception(response) as ExceptionModel;
            Assert.Equal(404, exception.error.StatusCode); 
        }

         /// <summary>
        ///  To test the login user by invalid data
        /// </summary>
        [Fact]
        public void login_UnauthorizedObjectResults()
        {
            UserDto loginDetails2 = new UserDto() { UserName = "Aarthi", Password = "12345eniyavan" };
             Action response = () => _authController.LoginUser(loginDetails2);
             ExceptionModel exception = Record.Exception(response) as ExceptionModel;
            Assert.Equal(404, exception.error.StatusCode); 
        }

       /// <summary>
        ///  To test the delete address book
        /// </summary>
        [Fact]
        public void deleteAddressBook_OkObjectResult()
        {
            Guid AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a");
            ActionResult response1 = _addressBookController.DeleteAddressBook(AddressBookId) as ActionResult;
            Assert.IsType<OkObjectResult>(response1);
            OkObjectResult item = response1 as OkObjectResult;
            Assert.IsType<string>(item.Value);
        }

          /// <summary>
        ///  To test the delete address book using non existing id
        /// </summary>
        [Fact]
        public void deleteAddressBook_NotFoundObjectResult()
        {
            Guid AddressBookId2 = Guid.Parse("7cf56f52-1aab-4646-b090-d337aac18355");
            Action response = () => _addressBookController.DeleteAddressBook(AddressBookId2);
             ExceptionModel exception = Record.Exception(response) as ExceptionModel;
            Assert.Equal(404, exception.error.StatusCode);
        }

        /// <summary>
        ///   To test the create method in the user
        /// </summary>
        // [Fact]
        // public void Create_AddressBook_OkResponses()
        // {
        //     CreateAddressBookDto addressBook = new CreateAddressBookDto()
        //     {
        //         FirstName = "pradeep",
        //         LastName = "kumar",
        //         Address = new List<AddressDto>(),
        //         Emails = new List<EmailDto>(),
        //         Phones = new List<PhoneDto>(),
        //     };
        //     addressBook.Address.Add(new AddressDto()
        //     {
        //         Line1 = "21 seval",
        //         Line2 = "nandavanam",
        //         City = "dindigul",
        //         ZipCode = "123123",
        //         StateName = "tamil nadu",
        //         Type = "PERSONAL",
        //         Country = "INDIA"
        //     });
        //     addressBook.Emails.Add(new EmailDto()
        //     {
        //         Email = "wefgqwdy@gmail.com",
        //         Type = "WORK"
        //     });
        //     user.Phones.Add(new CreatePhoneNumberDto()
        //     {
        //         PhoneNumber = "8189900490",
        //         Type = "PERSONAL"
        //     });

        //     ActionResult<string> account = _addresBookController.CreateAddressBook(user);
        //     Assert.IsType<OkObjectResult>(account.Result);
        // }

    }
}