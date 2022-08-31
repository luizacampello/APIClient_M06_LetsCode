using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ativ01Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        public List<Person> UserRepository { get; set; }

        public ValuesController()
        {

            UserRepository = new()
            {
                new Person("Matheus", "12365478911", DateTime.Parse("05/06/2002")),
                new Person("Mateus", "10624900614", DateTime.Parse("09/01/2003")),
                new Person("Luiza", "12365471911", DateTime.Parse("09/01/1996"))
            };
        }

        [HttpGet("person/getUserRepository")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<List<Person>> GetUserRepository()
        {
            return Ok(UserRepository);
        }

        [HttpPost("person/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Person> AddPerson(Person newPerson)
        {
            UserRepository.Add(newPerson);
            return StatusCode(201, newPerson);
        }

        [HttpPut("person/{userIndex}/editPersonInfoByIndex")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]

        public ActionResult<List<Person>> EditPersonInfoByIndex(int userIndex, Person changePerson)
        {
            if(userIndex >= UserRepository.Count || userIndex < 0)
            {
                return BadRequest();
            }
            
            List<Person> personChanges = new()
            {
                UserRepository[userIndex],
                changePerson
            };

            UserRepository[userIndex] = changePerson;

            return Accepted(personChanges);
        }

        [HttpPut("person/{personCPF}/editPersonInfoByCPF")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]

        public ActionResult<List<Person>> EditPersonInfoByCPF(string personCPF, Person changePerson)
        {
            int indexOf = UserRepository.FindIndex(person => person.CPF == personCPF);

            if (indexOf != -1)
            {
                List<Person> personChanges = new()
                {
                UserRepository[indexOf],
                changePerson
                };

                UserRepository[indexOf] = changePerson;

                return StatusCode(202, personChanges);
            }

            return NotFound();
        }

        [HttpDelete("person/{userIndex}/DeletePersonByIndex")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult DeletePersonByIndex(int userIndex)
        {
            if (userIndex >= UserRepository.Count || userIndex < 0)
            {
                return NotFound();
            }

            UserRepository.RemoveAt(userIndex);

            return NoContent();
        }

        [HttpDelete("person/{personCPF}/DeletePersonByCPF")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeletePersonByCPF(string personCPF)
        {
            int indexOf = UserRepository.FindIndex(person => person.CPF == personCPF);

            if (indexOf != -1)
            {
                UserRepository.RemoveAt(indexOf);
                return NoContent();
            }

            return NotFound();
            
        }
    }
}
