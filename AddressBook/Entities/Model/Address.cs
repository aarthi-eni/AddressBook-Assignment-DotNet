using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    [Table("address")]
    public class Address : BaseModel
    {
        [ForeignKey("AddressBook")]
        public Guid AddressBookId{get;set;}
        public string Line1 {get;set;}
        public string Line2{get;set;}
        public string City {get;set;}
        public string ZipCode { get;set;}
        public string StateName {get;set;}
        public string Type{get;set;}
        public string Country{get;set;}
    }
}