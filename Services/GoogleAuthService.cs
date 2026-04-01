using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Authentication;
using System.Web;
namespace StayFitz.Services
{
    public class GoogleAuthService
    {
        const string clientId = "YOUR_WEB_CLIENT_ID";

        //public async Task<string?> LoginAsync()
        //{
        //    var authUrl =
        //        $"https://accounts.google.com/o/oauth2/v2/auth" +
        //        $"?client_id={clientId}" +
        //        $"&redirect_uri=com.companyname.stayfitz:/oauth2redirect" +
        //        $"&response_type=token" +
        //        $"&scope=email%20profile";

        //    var callbackUrl = "com.companyname.stayfitz:/oauth2redirect";
       
        //    var result = await WebAuthenticator.AuthenticateAsync(
        //        new Uri(authUrl),
        //        new Uri(callbackUrl));

        //    if (result.Properties.TryGetValue("access_token", out var token))
        //    {
        //        return token;
        //    }

        //    return null;
        //}
    }
}
