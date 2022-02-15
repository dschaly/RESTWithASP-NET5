using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.DTO;
using RestWithASPNET.Model;
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

        // Method responsible for returning one person by ID
        public PersonDTO FindByID(long id) => _converter.Parse(_repository.FindByID(id));

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