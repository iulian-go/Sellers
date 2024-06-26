using SellersAPI.Context;
using SellersAPI.Interfaces;
using SellersAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IDbContext, DapperContext>();
builder.Services.AddScoped<IDistrictsRepository, DistrictsRepository>();
builder.Services.AddScoped<IShopsRepository, ShopsRepository>();
builder.Services.AddScoped<IVendorsRepository, VendorsRepository>();
builder.Services.AddScoped<IVendorDistrictRepository, VendorDistrictRepository>();

builder.Services.AddControllers();
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

//app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

app.UseAuthorization();
app.MapControllers();

app.Run();
