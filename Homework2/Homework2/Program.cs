using DataLayer.Contracts;
using DataLayer.ProductRepository;
using Domain.Contracts;
using Domain.Services;
using FluentValidation;
using Homework2.Interceptors;
using Homework2.Middleware;
using Homework2.Validators;
using Microsoft.OpenApi.Models;
using ProductGrpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<LogInterceptor>();
    options.Interceptors.Add<ErrorInterceptor>();
}).AddJsonTranscoding();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(Domain.Services.ProductService));

builder.Services.AddGrpcSwagger();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
});

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();

builder.Services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
builder.Services.AddScoped<IValidator<GetProductByIdRequest>, GetProductByIdRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateProductPriceRequest>, UpdateProductPriceRequestValidator>();
builder.Services.AddScoped<IValidator<GetProductsListWithFiltersRequest>, GetProductsListWithFiltersRequestValidator>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<Homework2.GrpcServices.ProductGrpcService>();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
