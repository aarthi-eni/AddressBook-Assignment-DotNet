using Entities.Dtos;
using Entities.Model;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Contracts.IServices;
using Contracts.IRepositories;
using CustomExceptionHandling;
using System.Text.RegularExpressions;
using Entities;

namespace Services
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IMapper _mapper;
        private readonly IAddressBookRepositories _addressBookRepositories;
        private readonly ILogger _logger;
        private readonly AddressBookContext _context;
        private string EmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public AddressBookService(IMapper mapper, IAddressBookRepositories addressBookRepositories, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _addressBookRepositories = addressBookRepositories ?? throw new ArgumentNullException(nameof(addressBookRepositories));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary>
        ///create new user in db
        ///</summary>
        ///<param name="userId"></param>
        // ///<param name="addressBookToCreate.Id"></param>
        public Guid CreateNewAddressBookUser(CreateAddressBookDto addressBook, Guid userId)
        {
            _logger.LogDebug("Processing request for CreateNewAddressBookUser ");
            IEnumerable<EmailAddress> email = _addressBookRepositories.GetAllEmails();
            IEnumerable<PhoneNumber> phone = _addressBookRepositories.GetAllPhone();
             if (addressBook == null)
            {
                _logger.LogError("Conflict Occurred");
                throw new ExceptionModel("Conflict Occurs", "AddressBook Name already exist",409);
            }
            foreach (EmailDto item in addressBook.Emails)
            {
                if (!Regex.IsMatch(item.Email, EmailRegex, RegexOptions.IgnoreCase))
                {
                    throw new ExceptionModel("Enter valid email", "Enter valid email to create an user", 400);
                }
                if (email.Any(x => x.Email == item.Email))
                {
                    _logger.LogError("Email already exist");
                    throw new ExceptionModel("Email already Exist", "conflict", 409);
                }
            }
            foreach (PhoneDto item in addressBook.Phones)
            {
                if (phone.Any(x => x.Phone == item.Phone))
                {
                    _logger.LogError("Phone number already exist");
                    throw new ExceptionModel("Phone number already Exist", "conflict", 409);
                }
            }
            AddressBook addressBookToCreate = new AddressBook()
            {
                Id = Guid.NewGuid(),
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                Createdon = DateTime.Now,
                Updatedon = DateTime.Now,
                CreatedBy = userId,
                UpdatedBy = userId,
                IsActive = true,
            };
           
            foreach (EmailDto item in addressBook.Emails)
            {
                EmailAddress EmailToCreate = new EmailAddress()
                {

                    Id = Guid.NewGuid(),
                    AddressBookId = addressBookToCreate.Id,
                    Email = item.Email,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = Guid.Empty,
                    IsActive = true,
                };
                _addressBookRepositories.CreateEmail(EmailToCreate);
            }
           
            foreach (AddressDto item in addressBook.Address)
            {
                Address AddressToCreate = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressBookId = addressBookToCreate.Id,
                    Line1 = item.Line1,
                    Line2 = item.Line2,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    StateName = item.StateName,
                    Country = item.Country,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = Guid.Empty,
                    IsActive = true,
                };
                _addressBookRepositories.CreateAddress(AddressToCreate);
            }
           
            foreach (PhoneDto item in addressBook.Phones)
            {
                 PhoneNumber PhoneToCreate = new PhoneNumber()
                {
                    Id = Guid.NewGuid(),
                    AddressBookId = addressBookToCreate.Id,
                    Phone = item.Phone,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = Guid.Empty,
                    IsActive = true,
                };
                _addressBookRepositories.CreatePhone(PhoneToCreate);
            }
            _addressBookRepositories.CreateAddressBook(addressBookToCreate);
            _addressBookRepositories.Save();
            _logger.LogDebug("Processing Completed for CreateNewAddressBookUser");
            _logger.LogInformation($"AddressBook created for the user with id {addressBookToCreate.Id}");
            return addressBookToCreate.Id;
        }

        ///<summary>
        ///Get All address book in database
        ///</summary>
        ///<param name="addressBooksByDto"></param>

        public IEnumerable<AddressBookDto> FetchAddressBookDetail()
        {
            _logger.LogDebug("Processing request for FetchAddressBookDetail ");
            IEnumerable<AddressBook> addressBooks = _addressBookRepositories.GetAllUsers();
             if (addressBooks == null)
            {
                _logger.LogInformation("No AddressBook Found");
                return null;
            }
            // IEnumerable<EmailAddress> email = _addressBookRepositories.GetAllEmails();
            // IEnumerable<Address> address = _addressBookRepositories.GetAllAddress();
            // IEnumerable<PhoneNumber> phone = _addressBookRepositories.GetAllPhone();
            // IEnumerable<Asset> asset = _addressBookRepositories.GetAllAsset();
            List<AddressBookDto> addressBooksByDto = new List<AddressBookDto>();

            // var x = (from addressbook in _context.AddressBook.Where(a=>a.IsActive)
            // join emails in _context.Email.Where(x => x.IsActive) on addressbook.Id equals emails.AddressBookId
            // join  addresss in _context.Address.Where(x => x.IsActive) on  addressbook.Id equals addresss.AddressBookId
            // join phones in _context.Phone.Where(x => x.IsActive) on addressbook.Id equals phones.AddressBookId
            // select new AddressBookDto()
            // {
            //     FirstName = addressbook.FirstName,
            //     Address = from a1 in addresss
            //     join
            // });                                                                                                                                                                                             
            
            // var x = (from addr in _addressBookRepositories.Find(y => y.IsActive)
            // join email in _emailRepository.Find(y => x.IsActive) on addr.Id equals email.AddressBookId
            // select new AddressBookDto()
            // {
            //     Id = addr.Id,
            //     Email = new EmailAddress()
            // })

            foreach (AddressBook addressBook in addressBooks)
            {
                AddressBookDto entity = new AddressBookDto()
                {
                    Id = addressBook.Id,
                    FirstName = addressBook.FirstName,
                    LastName = addressBook.LastName,
                    // Email = _mapper.Map<IEnumerable<EmailAddress>, IEnumerable<EmailDto>>(email.Where(a => a.AddressBookId == addressBook.Id)),
                    // Address = _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDto>>(address.Where(w => w.AddressBookId == addressBook.Id)),
                    // Phone = _mapper.Map<IEnumerable<PhoneNumber>, IEnumerable<PhoneDto>>(phone.Where(w => w.AddressBookId == addressBook.Id)),
                    // Asset = _mapper.Map<Asset, AssetDto>(asset.FirstOrDefault(w => w.AddressBookId == addressBook.Id)),

                };
                addressBooksByDto.Add(entity);
            }
            _logger.LogDebug("Processing Completed for FetchAddressBookDetail");
            _logger.LogInformation("AddressBook details fetched successfully");
            return addressBooksByDto;
        }

        ///<summary>
        /// Fetch all user from database
        ///</summary>
        public IEnumerable<AddressBook> GetCount()
        {
            return _addressBookRepositories.GetAllUsers();
        }

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="userId"></param>
        public AddressBookDto GetAddressBookById(Guid addressBookId)
        {
            _logger.LogDebug("Processing request for GetUserById");
            AddressBook addressBook = _addressBookRepositories.GetAddressBookById(addressBookId);
            if (addressBook is null)
            {
                _logger.LogError($"The addressbook id {addressBookId} was not found");
                throw new ExceptionModel("Id not found", "Id not found", 404); ;
            }
            AddressBook addressBooks = _addressBookRepositories.GetAddressBookById(addressBookId);
            IEnumerable<EmailAddress> email = _addressBookRepositories.GetEmailById(addressBookId);
            IEnumerable<Address> address = _addressBookRepositories.GetAddressById(addressBookId);
            IEnumerable<PhoneNumber> phone = _addressBookRepositories.GetPhoneById(addressBookId);
            Asset asset = _addressBookRepositories.GetAssetById(addressBookId);
            AddressBookDto addressBooksByDto = new AddressBookDto()
            {
                Id = addressBook.Id,
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                Email = _mapper.Map<IEnumerable<EmailAddress>, IEnumerable<EmailDto>>(email),
                Address = _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDto>>(address),
                Phone = _mapper.Map<IEnumerable<PhoneNumber>, IEnumerable<PhoneDto>>(phone),
                Asset = _mapper.Map<Asset, AssetDto>(asset),
            };
            _logger.LogDebug("Processing Completed for GetUserById");
            _logger.LogInformation("AddressBook details fetched successfully");
            return addressBooksByDto;
        }

        ///<summary>
        ///update address book details
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="userId"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userInput"></param>
        public void UpdateAddressBook(Guid addressBookId, CreateAddressBookDto addressBook, Guid userId)
        {
             if (addressBook is null)
            {
                 _logger.LogError($"The addressbook id {addressBookId} was not found");
                throw new ExceptionModel("AddressBook not found", "AddressBook not found", 404);
            }
            _logger.LogDebug("Processing request for UpdateAddressBook ");
            // IEnumerable<EmailAddress> email = _addressBookRepositories.GetAllEmails();
            // IEnumerable<PhoneNumber> phone = _addressBookRepositories.GetAllPhone();
            IEnumerable<EmailAddress> emails = _addressBookRepositories.GetEmailById(addressBookId);
            IEnumerable<Address> address = _addressBookRepositories.GetAddressById(addressBookId);
            IEnumerable<PhoneNumber> phones = _addressBookRepositories.GetPhoneById(addressBookId);
            foreach (Address item in address)
            {
                item.IsActive = false;
            }
            foreach (EmailAddress item in emails)
            {
                item.IsActive = false;
            }
            foreach (PhoneNumber item in phones)
            {
                item.IsActive = false;
            }
            foreach (EmailDto item in addressBook.Emails)
            {
                if (!Regex.IsMatch(item.Email, EmailRegex, RegexOptions.IgnoreCase))
                {
                    throw new ExceptionModel("Enter valid email", "Enter valid email to create an user", 400);
                }
                if (emails.Any(x => x.Email == item.Email))
                {
                    _logger.LogError("Email already exist");
                    throw new ExceptionModel("Email already Exist", "conflict", 409);
                }
            }
            foreach (PhoneDto item in addressBook.Phones)
            {
                if (phones.Any(x => x.Phone == item.Phone))
                {
                    _logger.LogError("Phone number already exist");
                    throw new ExceptionModel("Phone number already Exist", "conflict", 409);
                }
            }
            AddressBook addressBookToCreate = new AddressBook()
            {
                Id = addressBookId,
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                Createdon = DateTime.Now,
                Updatedon = DateTime.Now,
                CreatedBy = userId,
                UpdatedBy = userId,
                IsActive = true,
            };
            foreach (EmailDto item in addressBook.Emails)
            {
                EmailAddress EmailToCreate = new EmailAddress()
                {

                    AddressBookId = addressBookToCreate.Id,
                    Email = item.Email,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsActive = true,
                };
                _addressBookRepositories.CreateEmail(EmailToCreate);
            }
            
            foreach (AddressDto item in addressBook.Address)
            {
               Address AddressToCreate = new Address()
                {
                    AddressBookId = addressBookToCreate.Id,
                    Line1 = item.Line1,
                    Line2 = item.Line2,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    StateName = item.StateName,
                    Country = item.Country,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsActive = true,
                };
                _addressBookRepositories.CreateAddress(AddressToCreate);
            }
           
            foreach (PhoneDto item in addressBook.Phones)
            {
                PhoneNumber PhoneToCreate = new PhoneNumber()
                {
                    AddressBookId = addressBookToCreate.Id,
                    Phone = item.Phone,
                    Type = item.Type,
                    Createdon = DateTime.Now,
                    Updatedon = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsActive = true,
                };
                _addressBookRepositories.CreatePhone(PhoneToCreate);
            }
            _addressBookRepositories.UpdateAddressBook(addressBookToCreate);
            _addressBookRepositories.Save();
            _logger.LogInformation($"AddressBook created for the user with id {addressBookToCreate.Id}");
            _logger.LogDebug("Processing Completed for UpdateAddressBook");
        }

        /// <summary>
        /// Method to delete the addressbook by providing the id
        /// </summary>
        /// <param name="id">Id of the addressbook</param>
        /// <returns name="response">response whether the addressbook is deleted or not</returns>
        public void DeleteAddressBook(Guid addressBookId)
        {
            _logger.LogDebug($"Entering into the Delete AddressBook method");
            AddressBook addressBookData = _addressBookRepositories.GetAddressBookById(addressBookId);
             if (addressBookData is null)
            {
                _logger.LogError($"The addressbook id {addressBookId} was not found");
                throw new ExceptionModel("AddressBook not found", "AddressBook not found", 404);
            }
            IEnumerable<EmailAddress> email = _addressBookRepositories.GetEmailById(addressBookId);
            IEnumerable<Address> address = _addressBookRepositories.GetAddressById(addressBookId);
            IEnumerable<PhoneNumber> phone = _addressBookRepositories.GetPhoneById(addressBookId);
            Asset asset = _addressBookRepositories.GetAssetById(addressBookId);
            _logger.LogInformation($"The addressbook of {addressBookId} deleted successfully");
            addressBookData.IsActive = false;
            foreach (Address item in address)
            {
                item.IsActive = false;
            }
            foreach (EmailAddress item in email)
            {
                item.IsActive = false;
            }
            foreach (PhoneNumber item in phone)
            {
                item.IsActive = false;
            }
            _addressBookRepositories.Save();
            _logger.LogDebug($"AddressBook Deleting  process is compeleted for the {addressBookId}");
        }
    }
}

