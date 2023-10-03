using PromFuture.TestTask.Data;
using PromFuture.TestTask.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<CommandService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TerminalService>();
builder.Services.AddScoped<TerminalResponseService>();
builder.Services.AddScoped<TerminalRequestService>();
builder.Services.AddSingleton<ITokenStorageService, TokenStorageService>();
builder.Services.AddAntDesign();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://178.57.218.210:398/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
