using APIAuth.Models;
using APIAuth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [DI]
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<UserService>();
#endregion

builder.Services.Configure<TokenManagement>(builder.Configuration.GetSection("tokenManagement"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();