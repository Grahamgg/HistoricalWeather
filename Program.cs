using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<WeatherForecastService>();

var app = builder.Build();
//Commenting out this line for testing purposes.
//if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500; 
            context.Response.ContentType = "text/html";

            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null)
            {
                var exception = errorFeature.Error;
               
            }

            await context.Response.WriteAsync("<h1>Something went wrong, please try again later.</h1>");
        });
    });

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
