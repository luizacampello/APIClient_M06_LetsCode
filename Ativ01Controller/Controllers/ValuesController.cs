using Ativ01Controller.Core.Interfaces;
using Ativ01Controller.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ativ01Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(TimeResourceFilter))]
    public class ValuesController : ControllerBase
    {

        public IPersonService _personService { get; set; }

        public ValuesController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("person/getPersonRepository")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Person>> GetPersonRepository()
        {
            return Ok(_personService.GetPersonRepository());
        }

        [HttpGet("person/{personCPF}/getPersonByCPF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> GetPersonByCPF(string personCPF)
        {
            Person person = _personService.GetPersonByCPF(personCPF);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost("person/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [TypeFilter(typeof(PersonValidationActionFilter))]
        public ActionResult<Person> AddPerson(Person newPerson)
        {
            if (!_personService.AddPerson(newPerson))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(AddPerson), newPerson);
        }

        [HttpPut("person/{personCPF}/editPersonInfoByCPF")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [TypeFilter(typeof(UpdateValidationActionFilter))]
        public ActionResult<List<Person>> EditPersonInfoByCPF(string personCPF, Person changePerson)
        {
            Person person = _personService.GetPersonByCPF(personCPF);

            List<Person> personChanges = new()
            {
                person
            };

            if (person is not null)
            {
                if (_personService.EditPersonInfoByCPF(personCPF, changePerson))
                {
                    Person newPerson = _personService.GetPersonByCPF(changePerson.cpf);
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
            if (!_personService.DeletePersonById(userId))
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
            if (!_personService.DeletePersonByCPF(personCPF))
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
