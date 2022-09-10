using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace SignalRDemo.System.Aspects;

public class ExceptionHandlingInterceptor : IInterceptor
{
    private readonly ILogger<ExceptionHandlingInterceptor> _logger;

    public ExceptionHandlingInterceptor(ILogger<ExceptionHandlingInterceptor> logger)
    {
        _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "", invocation.Method.Name);
            throw;
        }
    }
}
