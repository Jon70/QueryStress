using Autofac;
using Autofac.Extensions.DependencyInjection;
using QueryStress.Core.Interfaces;
using QueryStress.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(diBuilder => new ApiApplicationLoader().Load(diBuilder));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/providers", (IProviderInfo[] providers) => providers.Select(x => x.Name));
app.MapPost("/connection/test", (ConnectionRequest r) => r);
app.Run();

public record ConnectionRequest(string ConnectionString, string Provider);
