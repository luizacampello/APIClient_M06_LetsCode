using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ativ01Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public List<Person> userRepository { get; set; }

        public ValuesController()
        {
            userRepository = new()
            {
                new Person("Mateus", "123654789-11", DateTime.Parse("09/01/2003")),
                new Person("Abobora", "123654719-11", DateTime.Parse("09/01/2002")),
                new Person("Luiza", "123654719-11", DateTime.Parse("09/01/1996"))
            };
        }

        [HttpGet]
        public List<Person> GetUserRepository()
        {
            return userRepository;
        }

        [HttpPost]

        public Person AddPerson(Person newPerson)
        {
            userRepository.Add(newPerson);
            return newPerson;
        }

        [HttpPut]

        public List<Person> EditPersonInfo(int userIndex, Person changePerson)
        {
            List<Person> personChanges = new()
            {
                userRepository[userIndex],
                changePerson
            };

            userRepository[userIndex] = changePerson;

            return personChanges;
        }

        [HttpDelete]

        public List<Person> DeletePerson(int userIndex)
        {
            userRepository.RemoveAt(userIndex);
            return userRepository;
        }
    }
}
