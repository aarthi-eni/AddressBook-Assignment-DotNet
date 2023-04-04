using System;
using Contracts.IRepositories;
using Entities;
using Entities.Model;
using System.Linq;
namespace Repositories
{
    public class AuthenticationRepositories: IAuthenticationRepositories
        {

          private readonly AddressBookContext _context;

        public AuthenticationRepositories(AddressBookContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        public UserLogin GetUserByUserName(string userName)
        {
            UserLogin user = _context.User.Where(a => a.UserName == userName && a.IsActive).FirstOrDefault();
            return user;
        }
    }     
        }