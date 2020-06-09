namespace WoWForDummies.WebApi.Controllers.Identity
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Infrastructure;
    using WoWForDummies.Dtos.Identity;
    using Common;
    using WoWForDummies.Services.Identity.Contracts;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly ApplicationSettings _appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<ApplicationSettings> appSettings)
        {
            this._userManager = userManager;
            this._identityService = identityService;
            this._appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestDto model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this._userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this._userManager.AddToRoleAsync(user, GlobalConstants.UserRole);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto model)
        {
            var user = await this._userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this._userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }

            var token = this._identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                this._appSettings.Secret);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}