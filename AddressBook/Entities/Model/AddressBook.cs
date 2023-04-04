using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    [Table("address_book")]
    public class AddressBook:BaseModel
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
    }
}