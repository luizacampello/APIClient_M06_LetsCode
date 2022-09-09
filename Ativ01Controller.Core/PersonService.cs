using Ativ01Controller.Core.Interfaces;

namespace Ativ01Controller.Core
{
    public class PersonService : IPersonService
    {
        public IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public bool AddPerson(Person newPerson)
        {
            return _personRepository.AddPerson(newPerson);
        }

        public bool DeletePersonByCPF(string personCPF)
        {
            return _personRepository.DeletePersonByCPF(personCPF);
        }

        public bool DeletePersonById(long userId)
        {
            return _personRepository.DeletePersonById(userId);
        }

        public bool EditPersonInfoByCPF(string personCPF, Person changePerson)
        {
            return _personRepository.EditPersonInfoByCPF(personCPF, changePerson);
        }

        public Person GetPersonByCPF(string personCPF)
        {
            return _personRepository.GetPersonByCPF(personCPF);
        }

        public List<Person> GetPersonRepository()
        {
            return _personRepository.GetPersonRepository();
        }
    }
}
