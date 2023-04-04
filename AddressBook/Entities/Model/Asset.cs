using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    [Table("asset")]
    public class Asset:BaseModel
    {
        [ForeignKey("AddressBooks")]
        public Guid AddressBookId{get;set;}
        public string FileName{get;set;}
        public string FileType{get;set;}
        public Byte[] ImageFile{get;set;}
        
    }
}