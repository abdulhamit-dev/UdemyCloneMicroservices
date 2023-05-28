using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Application.Consumers;
using Order.Application.Handlers;
using Order.Infrastructure;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// namespace OrderAPI
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             CreateHostBuilder(args).Build().Run();
//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 });
//     }
// }


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateOrderMessageCommandConsumer>();
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-order-service", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });
     
    });
});

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Order.Infrastructure");
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);


builder.Services.AddControllers(opt=>opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var orderDbContext = serviceProvider.GetRequiredService<OrderDbContext>();
    orderDbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();