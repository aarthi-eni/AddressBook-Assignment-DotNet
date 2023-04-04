using Entities.Dtos;
using System;

namespace Contracts.IServices
{
    public interface IAssetService
    {
        ///<summary>
        ///store and update details in asset 
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="userId"></param>
        ///<param name="file"></param>
        AssetResultDto StoreImage(AssetDto file, Guid AddressBookId, Guid userId);
    }
}