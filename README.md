## ✨ Recursos

- ✅ Suporte a comandos e queries com `Send()`
- ✅ Suporte a eventos com `Publish()`
- ✅ Pipeline de execução com behaviors (`IPipelineBehavior`)
- ✅ Baixo acoplamento com injeção de dependência
- ✅ Fácil de integrar com ASP.NET Core

---

## 📦 Instalação

### Usando referência de projeto:

1. Clone ou adicione sua biblioteca à solução
2. No projeto consumidor:

```bash
dotnet add reference ../SimpleMediator/SimpleMediator.csproj
```

---

## 🛠️ Configuração

No `Program.cs` ou `Startup.cs`:

```csharp
builder.Services.AddScoped<IMediator, SimpleMediator>();
builder.Services.AddScoped<IRequestExecutor, BehaviorRequestExecutor>();
builder.Services.AddScoped<INotificationExecutor, NotificationExecutor>();

// Registre seus handlers
builder.Services.AddScoped<IRequestHandler<PingRequest, string>, PingHandler>();
builder.Services.AddScoped<INotificationHandler<DomainEvent>, DomainEventHandler>();

// (Opcional) Behaviors
builder.Services.AddScoped<IPipelineBehavior<PingRequest, string>, LoggingBehavior>();
```

---

## 🚀 Exemplo de uso

### 1. Criando um comando/query

```csharp
public class PingRequest : IRequest<string>
{
    public string Message { get; set; } = "Ping";
}
```

### 2. Criando um handler

```csharp
public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}
```

### 3. Usando o Mediator

```csharp
var response = await mediator.Send(new PingRequest { Message = "Hello" });
Console.WriteLine(response); // → Pong: Hello
```

---

## ⚙️ Behaviors (middlewares)

### Criando um behavior de logging:

```csharp
public class LoggingBehavior : IPipelineBehavior<PingRequest, string>
{
    public async Task<string> Handle(PingRequest request, RequestHandlerDelegate<string> next)
    {
        Console.WriteLine($"[LOG] Antes: {request.Message}");
        var result = await next();
        Console.WriteLine($"[LOG] Depois: {result}");
        return result;
    }
}
```

Behaviors são executados em ordem de registro (como middlewares) e são ideais para:

- Logging
- Validação
- Retry
- Autorização
- Transações

---

## 📣 Publicação de eventos

### Criando um evento:

```csharp
public class UserCreatedEvent : INotification
{
    public Guid UserId { get; set; }
}
```

### Criando um handler de evento:

```csharp
public class SendEmailOnUserCreated : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification)
    {
        Console.WriteLine($"Enviando email para usuário {notification.UserId}");
        return Task.CompletedTask;
    }
}
```

### Publicando o evento:

```csharp
await mediator.Publish(new UserCreatedEvent { UserId = Guid.NewGuid() });
```

---

## 🤝 Contribuições

Sinta-se à vontade para abrir issues ou contribuir com melhorias!  
Este projeto nasceu como forma de estudo e alternativa pessoal ao MediatR.

---
