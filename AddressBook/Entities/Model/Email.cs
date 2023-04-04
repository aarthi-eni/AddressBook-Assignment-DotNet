using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
      [Table("email_address")]
      public class EmailAddress:BaseModel
    {
        [ForeignKey("AddressBooks")]
        public Guid AddressBookId{get;set;}
         public string Email{get;set;}
         public string Type{get;set;}
         
    }
}