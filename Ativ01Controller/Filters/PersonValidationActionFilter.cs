using Ativ01Controller.Core;
using Ativ01Controller.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ativ01Controller.Filters
{
    public class PersonValidationActionFilter : ActionFilterAttribute
    {
        public IPersonService _personService;
        public PersonValidationActionFilter(IPersonService personService)
        {
            _personService = personService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("newPerson", out var newPerson);

            Person personCPF = (Person)newPerson;

            if(personCPF != null)
            {
                if (_personService.GetPersonByCPF(personCPF.cpf) is not null)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
                }
            }           

        }
    }
}
