using Microsoft.Extensions.Configuration;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.IServices;
using Contracts.IRepositories;
using Entities.Dtos;
using Entities.Model;
using CustomExceptionHandling;

namespace Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationRepositories _userRepository;

        public AuthServices(IConfiguration config, IAuthenticationRepositories userRepository)
        {
            _userRepository = userRepository;
            _config = config;
        }


        ///<summary>
        ///create session token
        ///</summary>
        ///<param name="userData"></param>
        private string GenerateJWTToken(UserLogin userData)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecret:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[] {
         new Claim(JwtRegisteredClaimNames.Sub, userData.Id.ToString()),
         new Claim(JwtRegisteredClaimNames.Sub, userData.UserName),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(_config["JwtSecret:Issuer"],
                _config["JwtSecret:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(290),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        ///<summary>
         ///compare password
         ///</summary>
         ///<param name="dbPass"></param>
         ///<param name="userPass"></param>
         public bool ComparePassword(string userPass, string dbPass)
         {
             return userPass == dbPass ? true : false;
         }

        ///<summary>
        ///validate user input for login 
        ///</summary>
        ///<param name="userInput"></param>
        public TokenDto ValidateUserInputLogin(UserDto userInput)
        {
            UserLogin userFromRepo = _userRepository.GetUserByUserName(userInput.UserName);
            if (userFromRepo == null)
            {
                throw new ExceptionModel("UserName not exist", "User with this UserName not exist", 404);
                
            }

            if (!ComparePassword(userInput.Password, userFromRepo.Password))
            {
                throw new ExceptionModel("Incorrent password", "you have entered wrong password", 404);
            }

            return new TokenDto() { TokenType = "Bearer", AccessToken = GenerateJWTToken(userFromRepo) };
        }

    }
}