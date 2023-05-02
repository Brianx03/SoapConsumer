using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SoapConsumer;

public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var user = new ServiceReference1.User() { Id = 666, Name = context.Exception.Message };
        context.Result = new ObjectResult(user);
    }
}

