using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMVC.Data;
using SalesWebMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMVCContext"),
        new MySqlServerVersion(new Version(0, 0, 0, 0)),
        builder =>
        {
            builder.MigrationsAssembly("SalesWebMVC");
        }));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Adicione o SeedService ao pipeline de execução do aplicativo
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<SalesWebMVCContext>();
    var seedService = services.GetRequiredService<SeedingService>();
    seedService.Seed(); // Chame o método de seed para popular o banco de dados (ajuste o nome do método se necessário)
}

app.Run();
