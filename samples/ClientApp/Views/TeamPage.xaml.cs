using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ClientApp.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;


namespace ClientApp.Views
{
    public sealed partial class TeamPage : Page
    {
        private HttpClient httpClient = new HttpClient();

        public ObservableCollection<UserDto> Players { get; set; }

        public TeamPage()
        {
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadPlayers();
            base.OnNavigatedTo(e);
        }



        public async Task<string> GetAccessToken()
        {
            try
            {
                TokenCache.DefaultShared.Clear();
                var authority = string.Format(CultureInfo.InvariantCulture,
                    AppSettings.AADInstance, AppSettings.B2BTenant);
                var authContext = new AuthenticationContext(authority, true);
                var credentials = new ClientCredential(AppSettings.B2BClientId, AppSettings.B2BClientKey);
                var result = await authContext.AcquireTokenAsync(AppSettings.Audience, credentials);
                return result.AccessToken;
            }
            catch (Exception e)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog(e.Message, "Exception");
                await messageDialog.ShowAsync();
                throw e;
            }
        }


        private async Task LoadPlayers()
        {
            Players = Players ?? new ObservableCollection<UserDto>();
            Players.Clear();

            if (AppShell.Current.IsSigned)
            {
                try
                {
                    //Obtain access token
                    var accessToken = await GetAccessToken();

                    // Once the token has been returned by ADAL, add it to the http authorization header, before making the call to access the SPP API.
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    //Call SPP API to authenticate and get the Team Id
                    var url = $"{AppSettings.SPPApiBaseUrl}/api/v1/auth/{AppShell.Current.UserId}";
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        // Read the response and assign it to the Team Id
                        var teamId = await response.Content.ReadAsStringAsync();

                        //Now we obtain the list of players in the team
                        url = $"{AppSettings.SPPApiBaseUrl}/api/v1/teams/players/{teamId}";

                        response = await httpClient.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {

                            // Read the response and assign it to the Team Id
                            var content = await response.Content.ReadAsStringAsync();

                            //Deserialize the content into the list of players
                            var team = JsonConvert.DeserializeObject<TeamDto>(content);
                            title.Text = $"My Team is {team.Name}";

                            //Populate the players list
                            foreach (var user in team.Users)
                            {
                                user.PathtoPhoto = user.PathtoPhoto ?? "https://cdn1.iconfinder.com/data/icons/user/539/default-avatar.png";
                                Players.Add(user);
                            }
                        }
                    }
                    teamList.IsItemClickEnabled = true;
                }
                catch (Exception e)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog(e.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                teamList.IsItemClickEnabled = false;
                Players.Add(new UserDto
                {
                    FullName = "You must be first signed to get the list of players in your team!"
                });
            }
        }



    }
}
