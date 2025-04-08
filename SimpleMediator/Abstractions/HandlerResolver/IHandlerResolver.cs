namespace SimpleMediator.Core.Abstractions.HandlerResolver;

public interface IHandlerResolver
{
    object? Resolve(Type handlerType);
    IEnumerable<object> ResolveAll(Type handlerType);
}