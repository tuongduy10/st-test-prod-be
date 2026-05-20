namespace WebServer.Application.Abstractions.Messaging;

internal sealed class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;

    public Sender(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        var handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = _serviceProvider.GetRequiredService(handlerInterfaceType);
        var handleMethod = handlerInterfaceType.GetMethod("Handle")!;

        var behaviorInterfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
        var behaviors = _serviceProvider.GetServices(behaviorInterfaceType).Reverse().ToArray();

        RequestHandlerDelegate<TResponse> pipeline = () =>
            (Task<TResponse>)handleMethod.Invoke(handler, [request, cancellationToken])!;

        foreach (var behavior in behaviors)
        {
            var next = pipeline;
            var current = behavior;
            pipeline = () =>
            {
                var bHandleMethod = behaviorInterfaceType.GetMethod("Handle")!;
                return (Task<TResponse>)bHandleMethod.Invoke(current, [request, next, cancellationToken])!;
            };
        }

        return await pipeline();
    }
}
