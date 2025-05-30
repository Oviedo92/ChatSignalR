using ChatSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios
builder.Services.AddSignalR();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Estas dos líneas permiten servir HTML desde wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rutas
//app.MapHub<ClassHub>("/hubs/ClassHub");
app.MapHub<ClassHub>("/hubs/turnos");

app.MapRazorPages();

app.Run();
