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
    public class ActivateUserCommand :  IRequest<Result>
    {
        public string ModifiedByEmail { get; set; }
        public int UserId { get; set; }
    }

    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public ActivateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var userForActivation = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId);
            if (userForActivation == null)
            {
                return Result.Failure("User record does not exist");
            }
            userForActivation.LastModifiedDate = DateTime.UtcNow;
            userForActivation.LastModifiedByEmail = request.ModifiedByEmail;
            userForActivation.Status = Status.Activated;
            userForActivation.StatusDesc = Status.Activated.ToString();
            return Result.Success("User activated successfully", userForActivation);
        }
    }
}
