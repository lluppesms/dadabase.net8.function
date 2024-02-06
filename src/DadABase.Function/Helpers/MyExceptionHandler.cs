namespace Dadabase.Function.Helpers;

public class MyExceptionHandler : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var logger = context.GetLogger(context.FunctionDefinition.Name);
            logger.LogError($"Unexpected Error In {context.FunctionDefinition.Name}: {ex.Message}");
        }
    }
}
