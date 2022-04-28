using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PhoneAPI.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        private readonly OAuthAuthorizationServerOptions options;
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepository _repo = new UserMasterRepository())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    var checkWrong = _repo.CheckWrongUserName(context.UserName, context.Password);
                    if (checkWrong)
                    {
                        context.SetError("invalid_grant", "Tên tài khoản không tồn tại");
                    } else
                    {
                        context.SetError("invalid_grant", "Mật khẩu không đúng");
                    }
                    return;

                } else if(user.IsDelete == true)
                {
                    context.SetError("invalid_grant", "Tài khoản của bạn đã bị khoá");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                context.Validated(identity);
            }
        }
    }
}