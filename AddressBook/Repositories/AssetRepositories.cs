using System;
using Contracts.IRepositories;
using Entities;
using Entities.Model;
using System.Linq;
namespace Repositories
{
    public class AssetRepositories : IAssetRepositories
    {
       private readonly AddressBookContext _context;

        public AssetRepositories(AddressBookContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///upload user image to db
        ///</summary>
        ///<param name="saveImage"></param>
        public void UploadImage(Asset saveImage)
        {
            _context.Asset.Add(saveImage);
        }

        ///<summary>
        ///retrive image from db
        ///</summary>
        ///<param name="id"></param>
        public Asset RetriveImage(Guid id)
        {
            Asset image = _context.Asset.Where(a=>a.Id==id&&a.IsActive).FirstOrDefault(); 
            if (image == null)
                return null;

            return image;
        } 
    }
    
}
