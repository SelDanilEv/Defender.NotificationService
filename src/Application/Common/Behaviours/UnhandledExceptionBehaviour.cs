using Defender.NotificationService.Application.Helpers;
using MediatR;

namespace Defender.NotificationService.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    public UnhandledExceptionBehaviour()
    {
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            SimpleLogger.Log(
                $"Request: Unhandled Exception for Request {requestName} {request}\n" +
                $"Message : {ex.Message}");

            throw;
        }
    }
}
