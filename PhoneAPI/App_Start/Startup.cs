using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhoneAPI.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(PhoneAPI.App_Start.Startup))]

namespace PhoneAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
           
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/Api/AccountController/Login"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new MyAuthorizationServerProvider()
            };
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            /*OAuthAuthorizationServerOptions options2 = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/logout"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromMilliseconds(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new MyAuthorizationServerProvider()
            };
            //Token Generations
            app.UseOAuthAuthorizationServer(options2);*/
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }
    }
}
