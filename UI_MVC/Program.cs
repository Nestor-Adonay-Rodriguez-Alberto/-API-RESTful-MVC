var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Configura y agrega un cliente HTTP con nombre "API_RESTful"  * AGREGADA *
builder.Services.AddHttpClient("API_RESTful", x =>
{
    // Configura la dirección base del cliente HTTP desde la configuración
    x.BaseAddress = new Uri(builder.Configuration["UrlsAPI:Puerto"]);
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
