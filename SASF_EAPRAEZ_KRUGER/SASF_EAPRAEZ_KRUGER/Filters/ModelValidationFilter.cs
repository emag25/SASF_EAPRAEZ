using Microsoft.AspNetCore.Mvc.Filters;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;

namespace SASF_EAPRAEZ_KRUGER.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errores = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

                throw new ModelValidationException(errores);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}