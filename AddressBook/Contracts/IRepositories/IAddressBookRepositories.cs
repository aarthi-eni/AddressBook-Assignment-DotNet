using Entities.Model;
using System;
using System.Collections.Generic;

namespace Contracts.IRepositories
{
    public interface IAddressBookRepositories
    {
        ///<summary>
        ///get user count
        ///</summary>
        int GetCount();

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="userName"></param>
        // user operation
       // void CreateNewAddressBookUser(AddressBook user,EmailAddress email,Address address,PhoneNumber phone);
         void CreateAddress(Address address);

         void CreateEmail(EmailAddress email);

         void CreatePhone(PhoneNumber phone);

         void CreateAddressBook(AddressBook addressBook);
        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="user"></param>
        void UpdateAddressBook(AddressBook addressBook);

        void updateEmail(EmailAddress email);

        void updatePhone(PhoneNumber phone);

         public void updateAddress(Address address);


        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="id"></param>
        AddressBook GetUserById(Guid id);
        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        bool IsEmailExist(string email);

        ///<summary>
        ///is emai exist not check individual user
        ///</summary>
        ///<param name="email"></param>
        ///<param name="userId"></param>
        bool IsEmailExistUpdate(string email, Guid userId);

        ///<summary>
        ///check phone number exist
        ///</summary>
        ///<param name="phNumber"></param>
        bool IsPhoneExist(string phNumber);

        ///<summary>
        ///check phone number exist not check current user
        ///</summary>
        ///<param name="phNumber"></param>
        ///<param name="userId"></param>
        bool IsPhoneExistUpdate(string phNumber, Guid userId);

        ///<summary>
        ///get all emails, address,phoneNumber,Asset 
        ///</summary>
        IEnumerable<EmailAddress> GetAllEmails();
        IEnumerable<Address> GetAllAddress();
        IEnumerable<PhoneNumber> GetAllPhone();
        IEnumerable<Asset> GetAllAsset();

        List<EmailAddress> GetAllEmail();

        ///<summary>
        ///get all user from db
        ///</summary>
        IEnumerable<AddressBook> GetAllUsers();

        ///<summary>
        ///get email by user id
        ///</summary>
        ///<param name="id"></param>
        IEnumerable<EmailAddress> GetEmailById(Guid id);

        ///<summary>
        ///get address ids of user
        ///</summary>
        ///<param name="id"></param>
        IEnumerable<Address> GetAddressById(Guid id);

        ///<summary>
        ///get phone numeber ids of user
        ///</summary>
        ///<param name="id"></param>
        IEnumerable<PhoneNumber> GetPhoneById(Guid id);

        ///<summary>
        ///get asset ids of user
        ///</summary>
        ///<param name="id"></param>
        public Asset GetAssetById(Guid id);


        ///<summary>
        ///retrive image by user id
        ///</summary>
        ///<param name="id"></param>
        public Guid GetImageIdByUserId(Guid id);

        ///<summary>
        ///get assets by ids
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<Asset> GetAssetIds(Guid id);

        /// <summary>
        /// fetch addressbook by its id from the database
        /// </summary>
        /// <param name="id">addressbook id</param>
        /// <returns></returns>
        public AddressBook GetAddressBookById(Guid id);


        ///<summary>
        ///save all changes
        ///</summary>
        bool Save();
    }
}