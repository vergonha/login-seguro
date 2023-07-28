using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using secure_api.Data;
using secure_api.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("UserConnection")!;

builder.Services.AddDbContext<DataContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// O controller de Login necessita dos métodos de UserService, então injetamos essa dependência.
// A injeção sempre será feita com a interface e implementação, ou a classe ou uma instância dela.
builder.Services.AddScoped<IUserService, UserService>();
// A classe injetada é o UserService porque implementa a interface, mas se surgir a necessidade de mudar a lógica de login,
// Basta apenas alterar a forma com que o UserService implementa a interface.


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Eu gosto do swagger :(
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
