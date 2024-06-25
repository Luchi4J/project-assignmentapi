using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Azure.Messaging.ServiceBus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectAssignment.Application.User.Commands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Address { get; set; }
        public string CreatedByEmail { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userForCreation = await _context.Users.FirstOrDefaultAsync(a => a.FirstName == request.FirstName && 
             a.LastName == request.LastName && a.Email == request.Email);
            if (userForCreation != null)
            {
                return Result.Failure("User with these firstname, lastname and email details exist");
            }
            var user = new Domain.Entities.User
            {
                PhoneNumber = request.PhoneNumber,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Address = request.Address,
                Email = request.Email,
                CreatedDate = DateTime.UtcNow,
                CreatedByEmail = request.CreatedByEmail,
                Status = Status.Activated,
                StatusDesc = Status.Activated.ToString(),
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("User created successfully", user);
        }
    }
}
