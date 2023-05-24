using RadioCabs.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RCDbContext>(option =>
{
	//y.UseSqlServer(builder.Configuration["con"]);
	option.UseSqlServer(builder.Configuration.GetConnectionString("con"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Registrations}/{action=Create}/{id?}");

app.Run();
