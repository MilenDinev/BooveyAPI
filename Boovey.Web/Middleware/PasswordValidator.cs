namespace Boovey.Web.Middleware
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Validation;
    using Services.Managers.Interfaces;
    using Constants;

    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserManager userManager;
        public PasswordValidator(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await this.userManager.FindByNameAsync(context.UserName);

            if (user == null)
            {
                user = await this.userManager.FindByEmailAsync(context.UserName);
            }

            if (user != null)
            {
                var authResult = await this.userManager.ValidateUserCredentials(context.UserName, context.Password);

                if (authResult)
                {
                    var roles = await this.userManager.GetUserRolesAsync(user);
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    context.Result = new GrantValidationResult(subject: user.Id.ToString(), authenticationMethod: "password", claims: claims);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,ErrorMessages.InvalidCredentials);
                }

                return;
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, ErrorMessages.InvalidCredentials);
        }
    }
}


