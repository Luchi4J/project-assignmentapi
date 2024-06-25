using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Infrastructure.Persistence;
using ProjectAssignment.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Azure.Messaging.ServiceBus;
using MediatR;
using System.Reflection;


namespace ProjectAssignment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("UserMicroserviceDb"));
            }
            else
            {
               
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseCosmos(
                        accountEndpoint: configuration["ConnectionStrings:CosmosDBAccountEndpoint"],
                        accountKey: configuration["ConnectionStrings:CosmosDBAccountKey"],
                        databaseName: configuration["ConnectionStrings:CosmosDBName"]
                    );
                });
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var serviceBusConnectionString = configuration["AzureServiceBus:ConnectionString"];
                var queueName = configuration["AzureServiceBus:QueueName"];
                var mediator = provider.GetService<IMediator>();
                return new AzureMessagingSendingService(serviceBusConnectionString, queueName);
            });
            services.AddHostedService<AzureMessageReceivingService>();
            services.AddSingleton(_ => new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"]));
            services.AddSingleton(_ => new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"])
                                 .CreateReceiver(configuration["AzureServiceBus:QueueName"]
                                 , new ServiceBusReceiverOptions{
                                     PrefetchCount = 50,
                                     ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete,
                                     
                                 }));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateLifetime = true
                        };
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.Configuration = new OpenIdConnectConfiguration();
                    });
            services.AddAuthorization();
            services.AddProblemDetails();

            return services;
        }

    }

}

