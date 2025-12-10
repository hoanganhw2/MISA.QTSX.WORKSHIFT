using Microsoft.Extensions.Options;
using MISA.QTSX.CORE.interfaces.repository;
using MISA.QTSX.CORE.interfaces.services;
using MISA.QTSX.CORE.services;
using MISA.QTSX.infrastructure.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//dapper
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();
// Đăng ký CORS
builder.Services.AddCors(

        options => options.AddPolicy("AllowAll", policy =>

            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        )
    );
    
    

// Fix for CS0201: Assign true to MatchNamesWithUnderscores property
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
var app = builder.Build();
// Sử dụng CORS
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
