using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensibility;
using System.Web;

namespace StayFitz.SSO
{
    public class CustomBrowserWebUi : ICustomWebUi
    {
        //Success authentication html block
        private const string CloseWindowSuccessHtml = @"<html style='font-family: sans-serif;'>
  <head><title>Authentication Complete</title></head>
  <body style='text-align: center;'>
      <header>
          <h1>Custom Web UI</h1>
      </header>
      <main style='border: 1px solid lightgrey; margin: auto; width: 600px; padding-bottom: 15px;'>
          <h2 style='color: limegreen;'>Authentication complete</h2>
          <div>You can return to the application. Feel free to close this browser.</div>
      </main>
    
  </body>
</html>";

        //Failure authentication html block
        private const string CloseWindowFailureHtml = @"<html style='font-family: sans-serif;'>
  <head><title>Authentication Failed</title></head>
  <body style='text-align: center;'>
      <header>
          <h1>Custom Web UI</h1>
      </header>
      <main style='border: 1px solid lightgrey; margin: auto; width: 600px; padding-bottom: 15px;'>
          <h2 style='color: salmon;'>Authentication failed</h2>
          <div><b>Error details:</b> error {0} error_description: {1}</div>
          <br>
          <div>You can return to the application. Feel free to close this browser.</div>
      </main>
    
  </body>
</html>";

        public async Task<Uri> AcquireAuthorizationCodeAsync(
            Uri authorizationUri,
            Uri redirectUri,
            CancellationToken cancellationToken)
        {
            if (!redirectUri.IsLoopback)
            {
                throw new ArgumentException("Only loopback redirect uri is supported with this WebUI. Configure http://localhost or http://localhost:port during app registration. ");
            }

            Uri result = await InterceptAuthorizationUriAsync(authorizationUri, redirectUri, cancellationToken)
                .ConfigureAwait(true);

            return result;
        }

        public static string FindFreeLocalhostRedirectUri()
        {
            TcpListener listner = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listner.Start();
                int port = ((IPEndPoint)listner.LocalEndpoint).Port;
                return "http://localhost:" + port;
            }
            finally
            {
                listner?.Stop();
            }
        }

        private async static void OpenBrowser(string url)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
                {
                    await Browser.Default.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
                }
            }
        }

        /// <summary>
        /// Opens a new tab on the OS default browser and navigates to the authorization URI, while listening to its response.
        /// Then, displays an HTML block based on the authorization response.
        /// </summary>
        private async Task<Uri> InterceptAuthorizationUriAsync(
            Uri authorizationUri,
            Uri redirectUri,
            CancellationToken cancellationToken)
        {
            // Open the browser with the authorization URI
            OpenBrowser(authorizationUri.ToString());

            // Start listening for the authorization response
            using (var listener = new SingleMessageTcpListener(redirectUri.Port))
            {
                Uri authCodeUri = null;

                try
                {
                    // Create a timeout task
                    var timeoutTask = Task.Delay(80000, cancellationToken);

                    // Start listening for the response
                    var listenTask = listener.ListenToSingleRequestAndRespondAsync(
                        (uri) =>
                        {
                            Trace.WriteLine("Intercepted an auth code URL: " + uri.ToString());
                            authCodeUri = uri;

                            // Determine what to display in the browser after authentication
                            return GetMessageToShowInBroswerAfterAuth(uri);
                        },
                        cancellationToken);

                    // Wait for either the listener or the timeout
                    var completedTask = await Task.WhenAny(listenTask, timeoutTask);

                    if (completedTask == timeoutTask)
                    {
                        // Timeout occurred before completing authentication
                        throw new TimeoutException("Browser interaction timed out.");
                    }

                    // Await the listener task to ensure exceptions are propagated
                    await listenTask;

                    return authCodeUri;
                }
                catch (OperationCanceledException)
                {
                    // Handle the case where cancellation token is triggered
                    throw new TaskCanceledException("Browser interaction was canceled.");
                }
                catch (TimeoutException)
                {
                    // Handle the timeout specifically for user feedback
                    Trace.WriteLine("Browser interaction timed out.");
                    throw;
                }
            }
        }

        // Parses the authorization response and displays the success or failure HTML block accordingly
        private static string GetMessageToShowInBroswerAfterAuth(Uri uri)
        {
            // Parse the uri to understand if an error was returned. This is done just to show the user a nice error message in the browser.
            var authCodeQueryKeyValue = HttpUtility.ParseQueryString(uri.Query);

            string errorString = authCodeQueryKeyValue.Get("error");
            if (!string.IsNullOrEmpty(errorString))
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    CloseWindowFailureHtml,
                    errorString,
                    authCodeQueryKeyValue.Get("error_description"));
            }

            return CloseWindowSuccessHtml;
        }
    }
}
