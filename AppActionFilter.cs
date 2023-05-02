using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SoapConsumer;

public class AppActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey("userId"))
        {
            int userId = (int)context.ActionArguments["userId"];
            if (userId <= 0)
            {
                context.ActionArguments["userId"] = 1;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var user = (context.Result as ObjectResult)?.Value as ServiceReference1.User;

        if (user is not null)
        {
            if (!user.Email.Contains('@'))
                user.Email = user.Email + "@unosquare.com";
        }
        else
        {
            user = new ServiceReference1.User() { Id = 0, Name = "Not found" };
        }
        context.Result = new ObjectResult(user);
    }
}

