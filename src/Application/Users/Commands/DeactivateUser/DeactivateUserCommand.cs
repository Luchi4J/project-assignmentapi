using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Domain.Entities;
using ProjectAssignment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Application.Users.Commands
{
    public class DeactivateUserCommand : IRequest<Result>
    {
        public string ModifiedByEmail { get; set; }
        public int UserId { get; set; }
    }

    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public DeactivateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var userForDeactivation = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId);
            if (userForDeactivation == null)
            {
                return Result.Failure("User record does not exist");
            }
            userForDeactivation.LastModifiedDate = DateTime.UtcNow;
            userForDeactivation.LastModifiedByEmail = request.ModifiedByEmail;
            userForDeactivation.Status = Status.Deactivated;
            userForDeactivation.StatusDesc = Status.Deactivated.ToString();
            return Result.Success("User deactivated successfully", userForDeactivation);
        }
    }
}
