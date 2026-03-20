using HRMS.Components;
using HRMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = builder.Environment.IsDevelopment());

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
