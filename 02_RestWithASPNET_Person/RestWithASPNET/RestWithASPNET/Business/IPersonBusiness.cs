using RestWithASPNET.Data.DTO;
using RestWithASPNET.HyperMedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindByID(long id);
        List<PersonDTO> FindByName(string firstName, string lastName);
        List<PersonDTO> FindAll();
        PagedSearchDTO<PersonDTO> FindPaged(string name, string sortDirection, int pageSize, int page);
        PersonDTO Update(PersonDTO person);
        PersonDTO Disable(long id);
        void Delete(long id);
    }
}