using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Identity.Client;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ClientApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Redirect URI is the URI where Azure AD will return OAuth responses.
        // The Authority is the sign-in URL of the tenant.
        //
        public static string[] ApiScopes = { $"https://{AppSettings.B2CTenant}/tppapi/demo.read" };
        private static string BaseAuthority = "https://login.microsoftonline.com/tfp/{tenant}/{policy}/oauth2/v2.0/authorize";
        public static string Authority = BaseAuthority.Replace("{tenant}", AppSettings.B2CTenant)
            .Replace("{policy}", AppSettings.SignInPolicy);

        public PublicClientApplication PublicClientApp { get; } = new PublicClientApplication(
            AppSettings.B2CClientId, Authority);



        public SignInPage()
        {
            this.InitializeComponent();
            InitializeLogin();
        }

        private async Task<AuthenticationResult> InitializeLogin()
        {
            try
            {
                var result = await this.PublicClientApp.AcquireTokenAsync(
                    ApiScopes,
                    GetUserByPolicy(PublicClientApp.Users, AppSettings.SignInPolicy),
                    UIBehavior.ForceLogin,
                    null,
                    null,
                    Authority);

                //Display a welcome message
                txtMessage.Text = $"Welcome! You have been successfully signed it! Your Id is {result.UniqueId}";

                //Store the user's object ID in the global variable
                AppShell.Current.UserId = result.UniqueId;
                AppShell.Current.IsSigned = true;

                return result;
            }
            catch (Exception ex)
            {
                // An unexpected error occurred.
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "Inner Exception : " + ex.InnerException.Message;
                }
                this.txtMessage.Text = message;
            }

            return null;
        }



        private IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower())) return user;
            }

            return null;
        }


        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }



    }
}
