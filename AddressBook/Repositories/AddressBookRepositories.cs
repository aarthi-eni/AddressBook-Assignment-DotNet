using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.IRepositories;
using Entities;
using Entities.Model;
namespace Repositories
{
    public class AddressBookRepositories : IAddressBookRepositories
    {
     private readonly AddressBookContext _context;

        public AddressBookRepositories(AddressBookContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="user"></param>
        // user operation
        // public void CreateNewAddressBookUser(AddressBook addressBook,EmailAddress email,Address address,PhoneNumber phone)
        // {
        //     _context.AddressBook.Add(addressBook);
        //     _context.Email.Add(email);
        //     _context.Address.Add(address);
        //     _context.Phone.Add(phone);
        // }

        public void CreateAddress(Address address)
        {
            _context.Address.Add(address);
        }

         public void CreateEmail(EmailAddress email)
        {
            _context.Email.Add(email);
        }

         public void CreatePhone(PhoneNumber phone)
        {
            _context.Phone.Add(phone);
        }

         public void CreateAddressBook(AddressBook addressBook)
        {
            _context.AddressBook.Add(addressBook);
        }


        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="user"></param>
        public void UpdateAddressBook(AddressBook addressBook)
        {

            _context.AddressBook.Update(addressBook);
        }

        public void updateEmail(EmailAddress email)
        {
            _context.Email.Update(email);
        }

         public void updatePhone(PhoneNumber phone)
        {
            _context.Phone.Update(phone);
        }

         public void updateAddress(Address address)
        {
            _context.Address.Update(address);
        }

        ///<summary>
        ///save all changes
        ///</summary>
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        ///<summary>
        ///retrive image by user id
        ///</summary>
        ///<param name="id"></param>
        public Guid GetImageIdByUserId(Guid id)
        {
            return _context.Asset.Where(e => e.AddressBookId == id && e.IsActive).FirstOrDefault().Id;
        }

        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        public bool IsEmailExist(string email)
        {
            return _context.Email.Any(e => e.Email == email && e.IsActive);
        }

        ///<summary>
        ///is emai exist not check individual user
        ///</summary>
        ///<param name="email"></param>
        ///<param name="userId"></param>
        public bool IsEmailExistUpdate(string email, Guid userId)
        {
            return _context.Email.Any(e => e.Email == email && e.AddressBookId != userId && e.IsActive);
        }

        ///<summary>
        ///get all emails 
        ///</summary>
        public IEnumerable<EmailAddress> GetAllEmails()
        {
            return _context.Email.Where(a=>a.IsActive).ToList();
        }
         public IEnumerable<PhoneNumber> GetAllPhone()
        {
            return _context.Phone.Where(a=>a.IsActive).ToList();
        }
         public IEnumerable<Asset> GetAllAsset()
        {
            return _context.Asset.Where(a=>a.IsActive).ToList();
        }

        ///<summary>
        ///get email by user id
        ///</summary>
        ///<param name="id"></param>
        // public IEnumerable<EmailAddress> GetEmailById(Guid id)
        // {
        //     return _context.Email.Where(a => a.AddressBookId == id && a.IsActive).ToList();
        // }

        ///<summary>
        ///get all emails 
        ///</summary>
        public IEnumerable<EmailAddress> GetEmailById(Guid addressBookId)
        {
           return _context.Email.Where(a => a.AddressBookId == addressBookId && a.IsActive ).ToList();
        }

        ///<summary>
        ///get phone numeber ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<PhoneNumber> GetPhoneById(Guid addressBookId)
        {
            return _context.Phone.Where(a => a.AddressBookId == addressBookId && a.IsActive).ToList();
        }


        ///<summary>
        ///check phone number exist
        ///</summary>
        ///<param name="phNumber"></param>
        public bool IsPhoneExist(string phNumber)
        {
            return _context.Phone.Any(e => e.Phone == phNumber && e.IsActive);
        }

        ///<summary>
        ///check phone number exist not check current user
        ///</summary>
        ///<param name="phNumber"></param>
        ///<param name="userId"></param>
        public bool IsPhoneExistUpdate(string phNumber, Guid userId)
        {
            return _context.Phone.Any(e => e.Phone == phNumber && e.AddressBookId != userId && e.IsActive);
        }

        ///<summary>
        ///get phone numeber ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<PhoneNumber> GetPhoneIds(Guid id)
        {
            return _context.Phone.Where(a => a.AddressBookId == id && a.IsActive);
        }



        ///<summary>
        ///get address ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<Address> GetAddressById(Guid id)
        {
            return _context.Address.Where(a => a.AddressBookId == id && a.IsActive ).ToList();
        }

        public IEnumerable<Address> GetAllAddress()
        {
            //return _context.Address.Where(a => a.AddressBookId == id && a.IsActive );
             return _context.Address.Where(a=>a.IsActive).ToList();
        }

         ///<summary>
        ///get all emails 
        ///</summary>
        public List<EmailAddress> GetAllEmail()
        {
            return _context.Email.Where(a=>a.IsActive).ToList();
        }

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="id"></param>
        public AddressBook GetUserById(Guid id)
        {

            return _context.AddressBook.FirstOrDefault(b => b.Id == id && b.IsActive);
        }

        ///<summary>
        ///get all user from db
        ///</summary>
        public IEnumerable<AddressBook> GetAllUsers()
        {
            return _context.AddressBook.Where(a => a.IsActive).ToList();
        }

        ///<summary>
        ///get user count
        ///</summary>
        public int GetCount()
        {
            List<AddressBook> count = _context.AddressBook.Where(a=>a.IsActive).ToList();
            return count.Count;
        }

        ///<summary>
        ///check whether user exist
        ///</summary
        ///<param name="userId"></param>
        public bool IsUserExits(Guid userId)
        {

            return _context.AddressBook.Any(a => a.Id == userId && a.IsActive);
        }

        ///<summary>
        ///get asset by id
        ///</summary>
        ///<param name="id"></param>
        public Asset GetAssetById(Guid id)
        {
            return _context.Asset.FirstOrDefault(a=>a.AddressBookId==id && a.IsActive);
        }
        ///<summary>
        ///get assets by ids
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<Asset> GetAssetIds(Guid id)
        {
            IEnumerable<Asset> asset = _context.Asset.Where(a => a.AddressBookId == id && a.IsActive);
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            return asset;
        } 

         /// <summary>
        /// fetch addressbook by its id from the database
        /// </summary>
        /// <param name="id">addressbook id</param>
        /// <returns></returns>
        public AddressBook GetAddressBookById(Guid id)
        {
            AddressBook addressBook = _context.AddressBook
                                .FirstOrDefault(addressBook => addressBook.Id == id && addressBook.IsActive);
            return addressBook;
        }
    }
}