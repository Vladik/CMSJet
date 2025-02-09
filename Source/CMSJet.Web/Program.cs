using CMSJet.Web.Components;
using CMSJet.Core.Data;
using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Data.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Radzen Components.
builder.Services.AddRadzenComponents();

// Fetch Connection String from Configuration
string? connectionString = builder.Configuration.GetConnectionString("PostgresDbSB");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("PostgreSQL connection string is missing. Please configure 'PostgresDb' in appsettings.json.");
}

// Register Database Connection
builder.Services.AddSingleton(new Database(connectionString));

// Register Repositories
builder.Services.AddScoped<MigrationRepository>();

// Register Services
builder.Services.AddScoped<IMigrationService, MigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
