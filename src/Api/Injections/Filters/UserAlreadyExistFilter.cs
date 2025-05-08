namespace Api.Injections.Filters;

public sealed class UserAlreadyExistFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UserAlreadyExistException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = 409,
                Title = "User Already Exist",
                Detail = context.Exception.Message
            };

            context.Result = new NotFoundObjectResult(problemDetails);
            context.Exception = null;
        }
    }
}
