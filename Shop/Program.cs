using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Extensions;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//Transfered All services and connection string
//into another class name DependencyServiceExt.cs 
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

/*app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));*/

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
*/

using var scrope = app.Services.CreateScope();
var services = scrope.ServiceProvider;



app.Run();


