using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Entities.Dtos
{
    public class AddressBookDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        ///first name of user 
        ///</summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        ///<summary>
        ///last name of user 
        ///</summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        ///<summary>
        ///user name of user 
        ///</summary>
        // [JsonProperty(PropertyName = "user_name")]
        // public string UserName { get; set; }

         ///<summary>
        ///address list of user
        ///</summary>
        public IEnumerable<AddressDto> Address { get; set; } = new List<AddressDto>();

        ///<summary>
        ///email list of user 
        ///</summary>
        public IEnumerable<EmailDto> Email { get; set; } = new List<EmailDto>();

        ///<summary>
        ///phone number list of user
        ///</summary>
        public IEnumerable<PhoneDto> Phone { get; set; } = new List<PhoneDto>();

        ///<summary>
        ///user image 
        ///</summary>
        public AssetDto Asset { get; set; }=new AssetDto();
    }
}