using Ativ01Controller.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ativ01Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        public List<Person> UserRepository { get; set; }

        public PersonRepository _personRepository { get; set; }

        public ValuesController(IConfiguration configuration)
        {
            UserRepository = new List<Person>();
            _personRepository = new PersonRepository(configuration);

        }

        [HttpGet("person/getPersonRepository")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Person>> GetPersonRepository()
        {
            return Ok(_personRepository.GetPersonRepository());
        }

        [HttpGet("person/{personCPF}/getPersonByCPF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> GetPersonByCPF(string personCPF)
        {
            Person person = _personRepository.GetPersonByCPF(personCPF);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost("person/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Person> AddPerson(Person newPerson)
        {
            if (!_personRepository.AddPerson(newPerson))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(AddPerson), newPerson);
        }

        [HttpPut("person/{personCPF}/editPersonInfoByCPF")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult<List<Person>> EditPersonInfoByCPF(string personCPF, Person changePerson)
        {
            Person person = _personRepository.GetPersonByCPF(personCPF);

            List<Person> personChanges = new()
            {
                person
            };

            if (person is not null)
            {
                if (_personRepository.EditPersonInfoByCPF(personCPF, changePerson))
                {
                    Person newPerson = _personRepository.GetPersonByCPF(changePerson.cpf);
                    personChanges.Add(newPerson);
                    return StatusCode(202, personChanges);
                }
            }

            return NotFound();

        }

        [HttpDelete("person/{userId}/DeletePersonById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletePersonById(long userId)
        {
            if (!_personRepository.DeletePersonById(userId))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("person/{personCPF}/DeletePersonByCPF")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePersonByCPF(string personCPF)
        {
            if (!_personRepository.DeletePersonByCPF(personCPF))
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
