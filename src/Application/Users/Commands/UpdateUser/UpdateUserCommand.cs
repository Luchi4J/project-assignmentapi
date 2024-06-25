using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Domain.Entities;
using ProjectAssignment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace ProjectAssignment.Application.User.Commands
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ModifiedByEmail { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userForUpdate = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId);
            if (userForUpdate == null)
            {
                return Result.Failure("User record does not exist");
            }
            userForUpdate.PhoneNumber = request.PhoneNumber;
            userForUpdate.LastName = request.LastName;
            userForUpdate.FirstName = request.FirstName;
            userForUpdate.Address = request.Address;
            userForUpdate.Email = request.Email;
            userForUpdate.LastModifiedDate = DateTime.UtcNow;
            userForUpdate.LastModifiedByEmail = request.ModifiedByEmail;
           
            _context.Users.Update(userForUpdate);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success("User updated successfully", userForUpdate);
        }
    }
}
