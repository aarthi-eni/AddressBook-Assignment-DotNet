using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Entities.Dtos
{
    /// <summary>
    /// CreateAddressBookDto
    /// </summary>
    [DataContract]
    public  class CreateAddressBookDto 
    {
         public Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Emails
        /// </summary>
        [DataMember(Name = "emails", EmitDefaultValue = false)]
        public IEnumerable<EmailDto> Emails { get; set; }

        /// <summary>
        /// Gets or Sets Phones
        /// </summary>
        [DataMember(Name = "phones", EmitDefaultValue = false)]
        public IEnumerable<PhoneDto> Phones { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public IEnumerable<AddressDto> Address { get; set; }

        /// <summary>
        /// Gets or Sets Asset
        /// </summary>
        // [DataMember(Name = "asset", EmitDefaultValue = false)]
        // public AssetDto Asset { get; set; }
    }
}