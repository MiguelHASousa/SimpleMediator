using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Core.Abstractions.HandlerResolver;

namespace SimpleMediator.Core.Infrastructure.ServiceProviderHandler;

public class ServiceProviderHandlerResolver(IServiceProvider provider) : IHandlerResolver
{
    public object? Resolve(Type handlerType) => provider.GetService(handlerType);

    public IEnumerable<object?> ResolveAll(Type handlerType) => provider.GetServices(handlerType);
}