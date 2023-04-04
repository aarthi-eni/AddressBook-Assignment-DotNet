using System.ComponentModel.DataAnnotations;
using System;

namespace Entities.Model
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public Boolean IsActive { get; set; }=true;


    }
}