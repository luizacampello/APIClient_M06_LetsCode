using Ativ01Controller.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ativ01Controller.Filters
{
    public class UpdateValidationActionFilter : ActionFilterAttribute
    {
        public IPersonService _personService;

        public UpdateValidationActionFilter(IPersonService personService)
        {
            _personService = personService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Person personRequest = (Person)context.ActionArguments;
            Person person = _personService.GetPersonByCPF(personRequest.cpf);

            if (person is null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

        }
    }
}
