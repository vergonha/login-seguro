using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using secure_api.Data;
using secure_api.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("UserConnection")!;

builder.Services.AddDbContext<DataContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// O controller de Login necessita dos m�todos de UserService, ent�o injetamos essa depend�ncia.
// A inje��o sempre ser� feita com a interface e implementa��o, ou a classe ou uma inst�ncia dela.
builder.Services.AddScoped<IUserService, UserService>();
// A classe injetada � o UserService porque implementa a interface, mas se surgir a necessidade de mudar a l�gica de login,
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
