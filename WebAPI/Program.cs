using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});
    //.ConfigureWebHostDefaults(webBuilder =>
    //{
      //  webBuilder.UseStartup<Startup>();

    //});
// PostSharp ücretli 
// Add services to the container.
// Autofac, Ninject, Castlewindor, lightınject
// AOP = bir metadoun onunde- sonunda çalışan kod parcacıklarını  yazıyoruz. businessda yazılır 
builder.Services.AddControllers();

builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
//builder.Services.AddSingleton<IProductService,ProductManager>();

//builder.Services.AddSingleton<IProductDal, EfProductDal>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

