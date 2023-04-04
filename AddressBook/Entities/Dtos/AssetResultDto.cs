using Newtonsoft.Json;
using System;

namespace Entities.Dtos
{
    public class AssetResultDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        ///file name of user image
        ///</summary>
        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }
     
        ///<summary>
        ///size of user image
        ///</summary>
        public long Size { get; set; }

        ///<summary>
        ///content of user image
        ///</summary>
       // public string FileContent { get; set; }
    }
}