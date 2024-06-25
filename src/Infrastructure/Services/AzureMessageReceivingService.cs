using Azure.Core;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Infrastructure.Services
{
    public class AzureMessageReceivingService : BackgroundService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusReceiver _receiver;
        private readonly IMediator _mediator;

        public AzureMessageReceivingService(ServiceBusClient client, ServiceBusReceiver receiver, IMediator mediator)
        {
            _client = client;
            _receiver = receiver;
            _mediator = mediator;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _receiver.ReceiveMessageAsync();
                if (message != null)
                {
                    Console.WriteLine($"Received message: {message.Body}");
                    var messageBody = message.Body.ToString();
                    var request = JsonConvert.DeserializeObject<Request>(messageBody);
                    var response = await _mediator.Send(request);

                    await _receiver.CompleteMessageAsync(message);
                }
            }
        }
    }
}