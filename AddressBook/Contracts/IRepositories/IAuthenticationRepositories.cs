using Entities.Model;

namespace Contracts.IRepositories
{
    public interface IAuthenticationRepositories
    {
        ///<summary>
        /// street 1  of the user 
        ///</summary>
        ///<param name="UserName"></param>
        UserLogin GetUserByUserName(string Username );

    }
}