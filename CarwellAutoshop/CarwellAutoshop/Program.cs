using CarwellAutoshop.CustomException;
using CarwellAutoshop.Infrastructure;
using CarwellAutoshop.Infrastructure.Data;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;
using CarwellAutoshop.Profiles;
using CarwellAutoshop.Service;
using CarwellAutoshop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuestPDF.Infrastructure;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<GarageDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    ));

//builder.Services.AddDbContext<GarageDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        sqlOptions =>
//        {
//            sqlOptions.EnableRetryOnFailure(
//                maxRetryCount: 5,
//                maxRetryDelay: TimeSpan.FromSeconds(10),
//                errorNumbersToAdd: null
//            );
//        }
//)
//);

builder.Services.AddDbContext<GarageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql =>
    {
        sql.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        );
    }));


//builder.Services.AddDbContext<GarageDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
//    ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICustomerData, CustomerData>();
builder.Services.AddScoped<IJobcardData, JobcardData>();
builder.Services.AddScoped<IJobCardRemarkData, JobCardRemarkData>();
builder.Services.AddScoped<IMasterData, MasterData>();
builder.Services.AddScoped<IPaymentData, PaymentData>();
builder.Services.AddScoped<IVehicleData, VehicleData>();
builder.Services.AddScoped<IInvoiceData, InvoiceData>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IJobcardService, JobcardService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


var app = builder.Build();
app.UseCors("AllowAngular");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carwell API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
