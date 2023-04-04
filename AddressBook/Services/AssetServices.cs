using Entities.Dtos;
using Entities.Model;
using System.IO;
using System;
using AutoMapper;
using Contracts.IServices;
using Contracts.IRepositories;
using CustomExceptionHandling;
namespace Services
 {
     public class AssetServices : IAssetService
     {

         private readonly IMapper _mapper;
         private readonly IAddressBookRepositories _AddressBookRepositories;
         private readonly IAssetRepositories _AssetRepositories;

         public AssetServices(IMapper mapper, IAddressBookRepositories AddressBookRepositories,IAssetRepositories AssetRepositories)
         {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _AddressBookRepositories = AddressBookRepositories ?? throw new ArgumentNullException(nameof(AddressBookRepositories));
             _AssetRepositories= AssetRepositories ?? throw new ArgumentNullException(nameof(AssetRepositories));
         }



         ///<summary>
         ///store and update details in asset 
         ///</summary>
         ///<param name="authId"></param>
         ///<param name="userId"></param>
         ///<param name="file"></param>
         public AssetResultDto StoreImage(AssetDto file, Guid AddressBookId, Guid userId)
         {
             Guid fileId;
             long Lengthof = ((int)file.ImageFile);
              if (Lengthof< 0)
            {
                 throw new ExceptionModel("File Not Found", "File is Empty", 400);
            }
             using (MemoryStream ms = new MemoryStream())
             {
                 Asset ImageEntity = StoreImageInDb(ms, file, AddressBookId,userId);
                 fileId = ImageEntity.Id;
             }
             return new AssetResultDto() { Id = fileId, Size = ((int)file.ImageFile), FileName = file.FileName };
         }

         ///<summary>
         ///store image in database
         ///</summary>
         ///<param name="authId"></param>
         ///<param name="file"></param>
         ///<param name="ms"></param>
         ///<param name="userId"></param>
         public Asset StoreImageInDb(MemoryStream ms,  AssetDto file, Guid AddressBookId, Guid userId)
         {
             Asset ImageEntity = _mapper.Map<Asset>(file);
             ImageEntity.CreatedBy = userId;
             _AssetRepositories.UploadImage(ImageEntity);
             _AddressBookRepositories.Save();
             return ImageEntity;
         }
     }
 }