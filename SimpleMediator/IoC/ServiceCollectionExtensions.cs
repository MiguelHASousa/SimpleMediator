using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleMediator.Core.Abstractions.Executors;
using SimpleMediator.Core.Abstractions.HandlerResolver;
using SimpleMediator.Core.Abstractions.Mediator;
using SimpleMediator.Core.Infrastructure.Executor;
using SimpleMediator.Core.Infrastructure.Mediator;
using SimpleMediator.Core.Infrastructure.ServiceProviderHandler;

namespace SimpleMediator.Core.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimpleMediator(this IServiceCollection services)
    {
        services.TryAddScoped<IHandlerResolver, ServiceProviderHandlerResolver>();
        services.TryAddScoped<IRequestExecutor, BehaviorRequestExecutor>();
        services.TryAddScoped<INotificationExecutor, NotificationExecutor>();
        services.TryAddScoped<IMediator, Mediator>();

        return services;
    }
}
