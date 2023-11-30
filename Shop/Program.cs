using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto;
using Shop.Entities;
using Shop.Extensions;
using Shop.Services;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//Transfered All services and connection string
//into another class name DependencyServiceExt.cs 
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



using var scrope = app.Services.CreateScope();
var services = scrope.ServiceProvider;



app.Run();


