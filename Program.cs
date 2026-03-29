using HRMS.Components;
using HRMS.Services;
using HRMS.Services.WorkflowService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = builder.Environment.IsDevelopment());

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<HRMS.Services.WorkflowService.IWorkflowService, HRMS.Services.WorkflowService.WorkflowService>();
builder.Services.AddScoped<HRMS.Services.ExportService.IExportService, HRMS.Services.ExportService.ExportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseStaticFiles();

var supportedCultures = new[] { "en-US", "ar-AE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
