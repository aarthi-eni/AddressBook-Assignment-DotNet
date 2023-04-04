using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
     [Table("phone_number")]
     public class PhoneNumber:BaseModel
    {
         [ForeignKey("AddressBooks")]
        public Guid AddressBookId{get;set;}
         public string Phone{get;set;}
         public string Type{get;set;}
        
    }
}