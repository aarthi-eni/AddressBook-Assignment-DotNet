using Entities.Dtos;

namespace Contracts.IServices
{
    public interface IAuthServices
    {

        ///<summary>
        ///validate user input for login 
        ///</summary>
        ///<param name="userInput"></param>
        TokenDto ValidateUserInputLogin(UserDto userInput);
    }
}