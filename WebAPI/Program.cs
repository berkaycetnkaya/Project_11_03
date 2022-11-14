using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Core.DependencyResolvers;

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

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//ServiceTool.Create(services);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)


        };
    });
//ServiceTool.Create(services);
            //    services.AddDependencyResolvers(new ICoreModule[]
            //{
            //    new CoreModule(),
            //});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();    

app.UseAuthorization();

app.MapControllers();

app.Run();

