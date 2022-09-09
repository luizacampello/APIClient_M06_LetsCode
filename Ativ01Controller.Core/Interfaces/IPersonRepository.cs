namespace Ativ01Controller.Core.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> GetPersonRepository();

        Person GetPersonByCPF(string personCPF);

        bool AddPerson(Person newPerson);

        bool EditPersonInfoByCPF(string personCPF, Person changePerson);

        bool DeletePersonById(long userId);

        bool DeletePersonByCPF(string personCPF);
    }
}
