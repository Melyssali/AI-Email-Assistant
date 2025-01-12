using Google.Apis.Gmail.v1;
using OpenMindProject.Services.Gmail;
using OpenMindProject.Services.ApiKey;
using OpenMindProject.Services.FileReader;
using OpenMindProject.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<FileReaderService>();
builder.Services.AddSingleton<ApiKeyService>();
builder.Services.AddScoped<GmailAuthService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<GmailService>(provider =>
{
    var AuthService = provider.GetRequiredService<GmailAuthService>();
    return AuthService.AuthenticateGmail();
});
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
app.MapRazorPages();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Email}/{action=Index}/{id?}");

app.Run();
