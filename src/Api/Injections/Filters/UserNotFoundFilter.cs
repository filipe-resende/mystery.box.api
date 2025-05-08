namespace Api.Injections.Filters;

public class UserNotFoundFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UserNotFoundException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = 404,
                Title = "User Not Found",
                Detail = context.Exception.Message
            };

            context.Result = new NotFoundObjectResult(problemDetails);
            context.Exception = null;
        }
    }
}
