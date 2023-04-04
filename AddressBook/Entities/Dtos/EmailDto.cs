using System;
using System.Runtime.Serialization;

namespace Entities.Dtos
{
     [DataContract]
      public  class EmailDto 
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        ///user id of who created address book
        ///</summary>
        public Guid AddressBookId { get; set; }

        ///<summary>
        ///unique email address of user
        ///</summary>
        [DataMember(Name="email")]
        public string Email{ get; set; }

        ///<summary>
        ///email address type
        ///</summary>
        [DataMember(Name="type")]
        public string Type{ get; set; }
    }
}