using RestWithASPNET.Data.DTO;
using System.Collections.Generic;

namespace RestWithASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindByID(long id);
        List<PersonDTO> FindAll();
        PersonDTO Update(PersonDTO person);
        PersonDTO Disable(long id);
        void Delete(long id);
    }
}