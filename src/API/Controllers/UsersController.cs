using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectAssignment.API.Controllers;
using ProjectAssignment.Application.ChequeLeaves.Queries;
using ProjectAssignment.Application.Common.Models;
using ProjectAssignment.Application.User.Commands;
using ProjectAssignment.Application.Users.Commands;
using ProjectAssignment.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        protected readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly AzureMessagingSendingService _messagingService;

        public UsersController(IHttpContextAccessor httpContextAccessor, AzureMessagingSendingService messagingService)
        {
            _httpContextAccessor = httpContextAccessor;
            _messagingService = messagingService;
        }
       

        /// <summary>
        /// This api allows you to create user
        /// </summary>
        /// <param name="command">The create user command</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("createuser")]
        public async Task<ActionResult> CreateUser(CreateUserCommand createUserCommand)
        {
            await _messagingService.SendMessageAsync(JsonConvert.SerializeObject(createUserCommand));
            return Ok("Create user request created successfully");
        }

        /// <summary>
        /// This api allows you to update user
        /// </summary>
        /// <param name="command">The update user command</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("updateuser")]
        public async Task<ActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            await _messagingService.SendMessageAsync(JsonConvert.SerializeObject(updateUserCommand));
            return Ok("Update user request sent successfully");
        }

        /// <summary>
        /// This api allows you to deactivate user
        /// </summary>
        /// <param name="command">The deactivate user command</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("deactivateuser")]
        public async Task<ActionResult> DeactivateUser(DeactivateUserCommand deactivateUserCommand)
        {
            await _messagingService.SendMessageAsync(JsonConvert.SerializeObject(deactivateUserCommand));
            return Ok("Deactivate user request sent successfully");
        }

        /// <summary>
        /// This api allows you to activate user
        /// </summary>
        /// <param name="command">The activate user command</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("activateuser")]
        public async Task<ActionResult> ActivateUser(ActivateUserCommand activateUserCommand)
        {
            await _messagingService.SendMessageAsync(JsonConvert.SerializeObject(activateUserCommand));
            return Ok("Activate user request sent successfully");
        }

        
        /// <summary>
        /// This api retrieves user by id
        /// </summary>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpGet("getuserbyid")]
        public async Task<ActionResult> GetUserById(int id)
        {
            await _messagingService.SendMessageAsync(JsonConvert.SerializeObject(id));
            return Ok("Get user by id request sent successfully");
        }
    }
}
