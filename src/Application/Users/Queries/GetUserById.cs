using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Domain.Entities;
using ProjectAssignment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Application.ChequeLeaves.Queries
{
    public class GetUserById :  IRequest<Result>
    {
        public int UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserById, Result>
    {
        private readonly IApplicationDbContext _context;
        public GetUserByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId);
            if (user == null)
            {
                return Result.Failure("User record does not exist");
            }
            return Result.Success("User retrieved successfully");
        }
    }

}
