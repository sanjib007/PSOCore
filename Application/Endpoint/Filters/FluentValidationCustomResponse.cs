using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Filters
{
    public class FluentValidationCustomResponse : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                       ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var value = context.ModelState.Keys.ToList();
                Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
                foreach (var modelStateKey in context.ModelState.Keys.ToList())
                {
                    string[] arr = null;
                    List<string> list = new List<string>();
                    foreach (var error in context.ModelState[modelStateKey].Errors)
                    {
                        list.Add(error.ErrorMessage);
                    }
                    arr = list.ToArray();
                    dictionary.Add(modelStateKey, arr);
                }
                var responseObj = new
                {
                    StatusCode = "400",
                    Message = "Bad Request",
                    Errors = dictionary
                };


                context.Result = new BadRequestObjectResult(responseObj);
                return;
            }
            await next();
        }
    }

}
