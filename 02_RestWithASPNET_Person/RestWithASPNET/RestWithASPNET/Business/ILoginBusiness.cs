using RestWithASPNET.Data.DTO;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenDTO ValidateCredentials(UserDTO user);
        TokenDTO ValidateCredentials(TokenDTO token);
        bool RevokeToken(string userName);
    }
}
