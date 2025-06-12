using Microsoft.EntityFrameworkCore;
using MythTechTest.Data;
using MythTechTest.Repositories;
using MythTechTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<SportsEventIngestionService>();
builder.Services.AddDbContext<SportsEventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MythAPI")));
builder.Services.AddScoped<ISportsEventRepository, SportsEventRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("forSPAinclusion", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000") // dummy urls for spa integration per spec
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ingestionService = services.GetRequiredService<SportsEventIngestionService>();
    var configuration = services.GetRequiredService<IConfiguration>();
    var jsonFileUrl = configuration["JsonFileUrl"];

    await ingestionService.IngestDataFromEndpoint(jsonFileUrl);
}

app.UseHttpsRedirection();


app.UseCors("forSPAinclusion"); 

app.UseAuthorization();
app.MapControllers();

app.Run();