using Entities.Model;
using System;

namespace Contracts.IRepositories
{
    public interface IAssetRepositories
    {
      ///<summary>
        /// street 1  of the user 
        ///</summary>
        ///<param name="id"></param>
        ///<Result>
        Asset RetriveImage(Guid id);

        ///<summary>
        /// 
        ///</summary>
        ///<param name="uploadImage"></param>
        void UploadImage(Asset uploadImage);  
    }
}