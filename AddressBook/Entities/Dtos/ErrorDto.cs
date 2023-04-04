using System.Runtime.Serialization;

namespace Entities.Dtos
{
    [DataContract]
    public class ErrorDto
    {
        ///<summary>
        /// detailed error message 
        ///</summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        ///<summary>
        /// detailed error code 
        ///</summary>
        [DataMember(Name = "Status_code")]
        public int StatusCode { get; set; }

        ///<summary>
        /// detailed error type 
        ///</summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}