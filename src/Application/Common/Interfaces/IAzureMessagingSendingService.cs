using ProjectAssignment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Application.Common.Interfaces
{
    public interface IAzureMessagingSendingService
    {
        Task SendMessageAsync(string message);

    }
}
