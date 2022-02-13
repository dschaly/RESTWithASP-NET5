using RestWithASPNET.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenDTO ValidateCredentials(UserDTO user);
        TokenDTO ValidateCredentials(TokenDTO token);
    }
}
