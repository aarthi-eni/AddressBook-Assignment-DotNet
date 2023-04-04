namespace Entities.Dtos
{
    public class ErrorResponse
    {
        ///<summary>
        /// detailed error message 
        ///</summary>
     
        public int status_code { get; set; } 

        public string description { get; set; }

        public string message { get; set; }
    }
}