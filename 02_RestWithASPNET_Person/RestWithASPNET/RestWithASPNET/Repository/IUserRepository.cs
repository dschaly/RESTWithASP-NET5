using RestWithASPNET.Data.DTO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserDTO user);
    }
}
