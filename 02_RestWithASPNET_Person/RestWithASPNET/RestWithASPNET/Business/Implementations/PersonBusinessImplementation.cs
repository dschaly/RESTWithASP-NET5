using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.DTO;
using RestWithASPNET.HyperMedia.Utils;
using RestWithASPNET.Repository;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        // Method responsible for returning all people,
        public List<PersonDTO> FindAll() => _converter.Parse(_repository.FindAll());
        public PagedSearchDTO<PersonDTO> FindPaged(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" :  "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @$"SELECT * FROM person P WHERE 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query += $"AND P.FirstName LIKE '%{name}%' ";
            query += $"ORDER BY P.FirstName {sort} LIMIT {size} OFFSET {offset}";

            string countQuery = @$"SELECT COUNT(*) FROM Person P WHERE 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery += $"AND P.FirstName LIKE '%{name}%'";

            var persons = _repository.FindPaged(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonDTO> { 
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                TotalResults = totalResults
            };
        }

        // Method responsible for returning one person by ID
        public PersonDTO FindByID(long id) => _converter.Parse(_repository.FindByID(id));

        // Method responsible for returning one person by First and/or Last Name
        public List<PersonDTO> FindByName(string firstName, string lastName) => _converter.Parse(_repository.FindByName(firstName, lastName));

        // Method responsible to crete one new person
        public PersonDTO Create(PersonDTO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for updating one person
        public PersonDTO Update(PersonDTO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        // Method responsible for disabling a person from and ID
        public PersonDTO Disable(long id) => _converter.Parse(_repository.Disable(id));

    }
}