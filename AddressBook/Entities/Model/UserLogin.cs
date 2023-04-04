using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    [Table("user_login")]
    public class UserLogin:BaseModel
    {
         [ForeignKey("AddressBooks")]
        public Guid AddressBookId{get;set;}
       public string UserName { get; set;}
       public string Password { get; set;}

    }
}   