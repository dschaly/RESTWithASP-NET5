using Microsoft.IdentityModel.JsonWebTokens;
using RestWithASPNET.Configurations;
using RestWithASPNET.Data.DTO;
using RestWithASPNET.Repository;
using RestWithASPNET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestWithASPNET.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;

        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUserRepository repository, ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenDTO ValidateCredentials(UserDTO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GererateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpire);

            _repository.RefreshUserInfo(user);

            DateTime createdDate = DateTime.Now;
            DateTime expirationDate = createdDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenDTO(
                true,
                createdDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken, 
                refreshToken
                );
        }

        public TokenDTO ValidateCredentials(TokenDTO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;

            var user = _repository.ValidateCredentials(userName);

            if (user == null || 
                user.RefreshToken != refreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GererateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenDTO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }
    }
}
