using ProjectAssignment.Application.Common.Interfaces;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System.Text.Json.Serialization;

namespace ProjectAssignment.Application.ChequeLeaves.Queries
{
    public class GetAllUsers : IRequest<Result>
    {
        public string LeafNumber { get; set; }
        public string AccountNumber { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsers, Result>
    {
        private readonly IApplicationDbContext _context;
        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync();
            if (users == null)
            {
                return Result.Failure("Users does not exist");
            }
            return Result.Success("Users retrieved successfully");
        }
    }
}
