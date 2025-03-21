using PWA_PN.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevelopment", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddSingleton<IPushSubscriptionStore, InMemoryPushSubscriptionStore>();
builder.Services.AddSingleton<IVapidTokensStorage, VapidTokensStorage>();
builder.Services.AddScoped<IPushNotificationsQueue, PushNotificationsQueue>();
builder.Services.AddScoped<IPushNotificationsService, PushNotificationsService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDevelopment");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
