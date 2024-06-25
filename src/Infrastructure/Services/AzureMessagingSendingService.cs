using Azure.Core;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProjectAssignment.Application.Common.Interfaces;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Infrastructure.Services
{
    public class AzureMessagingSendingService : IAzureMessagingSendingService 
    {
        private readonly IQueueClient _queueClient;

        public AzureMessagingSendingService(string serviceBusConnectionString, string queueName)
        {
            _queueClient = new QueueClient(serviceBusConnectionString, queueName);
        }

        public async Task SendMessageAsync(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var sentMessage = new Message
            {
                SessionId = Guid.NewGuid().ToString(),
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
            };
            await _queueClient.SendAsync(sentMessage);

        }
     

    }
}

