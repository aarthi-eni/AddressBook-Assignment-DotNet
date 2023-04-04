using Entities.Dtos;
using System;
using System.Collections.Generic;
using Entities.Model;

namespace Contracts.IServices
{
    public interface IAddressBookService
    {
        ///<summary>
        ///create new user in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="user"></param>
        Guid CreateNewAddressBookUser(CreateAddressBookDto user, Guid authId);

        ///<summary>
        ///delete address book in database
        ///</summary>
        ///<param name="user"></param>
        void DeleteAddressBook(Guid userId);

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="userId"></param>
        AddressBookDto GetAddressBookById(Guid AddressBookId);


        ///<summary>
        ///fetch all user from database
        ///</summary>
        IEnumerable<AddressBook> GetCount();


        ///<summary>
        ///update address book details
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="userId"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userInput"></param>
        void UpdateAddressBook(Guid userId, CreateAddressBookDto user, Guid authId);

        ///<summary>
        ///validate user input in create user 
        ///</summary>
        ///<param name="user"></param>
        IEnumerable<AddressBookDto> FetchAddressBookDetail();

    }
}