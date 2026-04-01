using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;
using StayFitz.Helper;
using StayFitz.SSO;

namespace StayFitz.Utilities
{
    public static class SSOHelper
    {
        private static PublicClientApplicationOptions appConfiguration = null;
        private static IConfiguration configuration;
        private static string graphURL;

        private static IPublicClientApplication application;

        private static string IdToken;

        // Timeout for waiting for the browser 
        private const int TimeoutWaitingForBrowserMs = 30 * 1000; // 30 seconds

        public static async Task SSOLogin()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("appsettings.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                // Build configuration from JSON asynchronously.
                var builder = new ConfigurationBuilder()
                    .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
                configuration = builder.Build();

                // Retrieve app-specific settings.
                appConfiguration = configuration.Get<PublicClientApplicationOptions>();
                graphURL = configuration.GetValue<string>("GraphApiUrl");

                //GraphServiceClient graphClient = await SignInAndInitializeGraphServiceClient(appConfiguration, new[] { "user.read" });
                //await CallMSGraph(graphClient);

            }
            catch (Exception ex)
            {
                // Handle errors, making sure it's done on the UI thread if needed.
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Sign in user using MSAL and obtain a token for MS Graph.
        /// </summary>
        /// <returns></returns>
        //private async static Task<GraphServiceClient> SignInAndInitializeGraphServiceClient(PublicClientApplicationOptions configuration, string[] scopes)
        //{
        //    GraphServiceClient graphClient = new GraphServiceClient(graphURL,
        //        new DelegateAuthenticationProvider(async (requestMessage) =>
        //        {
        //            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", await SignInUserAndGetTokenUsingMSAL(configuration, scopes));
        //        }));

        //    return await Task.FromResult(graphClient);
        //}

        /// <summary>
        /// Signs in the user using the device code flow and obtains an Access token for MS Graph.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        private static async Task<string> SignInUserAndGetTokenUsingMSAL(PublicClientApplicationOptions configuration, string[] scopes)
        {

#if ANDROID

            // build the AAd authority Url
            string authority = string.Concat(configuration.Instance, configuration.TenantId);

            // Initialize the MSAL library by building a public client application.
            application = PublicClientApplicationBuilder.Create(configuration.ClientId)
                                                    .WithAuthority(authority)
                                                    .WithRedirectUri(CustomBrowserWebUi.FindFreeLocalhostRedirectUri()) // required for CustomBrowserWebUi
                                                    .Build();
            AuthenticationResult result;

            try
            {
                var accounts = await application.GetAccountsAsync();

                // Try to acquire an access token from the cache, if UI interaction is required, MsalUiRequiredException will be thrown.
                result = await application.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();

            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    // Acquiring an access token interactively using custom web UI.
                    result = await application.AcquireTokenInteractive(scopes)
                        .WithParentActivityOrWindow(Platform.CurrentActivity) // Provide current Android activity
                        .WithCustomWebUi(new CustomBrowserWebUi()) // Custom Web UI interaction
                        .ExecuteAsync();
                }
                catch (TaskCanceledException)
                {
                    throw new TimeoutException("The browser interaction was not completed in time.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Login error: {ex.Message}");
                    throw;
                }

            }
            GlobalHelper.IdToken = result.IdToken;
            return result.AccessToken;
#else     
            return string.Empty;
#endif     

        }

        /// <summary>
        /// Call MS Graph and print results.
        /// </summary>
        /// <param name="graphClient"></param>
        /// <returns></returns>
        //    private static async Task CallMSGraph(GraphServiceClient graphClient)
        //    {
        //        try
        //        {
        //            var whoAmI = graphClient.Me.Client;

        //            var me = await graphClient.Me.Request().GetAsync();

        //        }
        //        catch (Exception ex)
        //        {
        //            var exToString_ = ex.ToString();
        //        }
        //    }
        //}
    }
}
