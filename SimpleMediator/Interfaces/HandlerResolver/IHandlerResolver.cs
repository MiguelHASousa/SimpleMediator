namespace SimpleMediator.Core.Interfaces.HandlerResolver;

public interface IHandlerResolver
{
    object? Resolve(Type handlerType);
    IEnumerable<object> ResolveAll(Type handlerType);
}