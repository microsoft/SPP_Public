using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPP.Core.Services.Impl;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Configuration;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Models;
using CognitiveService = TPP.Core.Services.Impl.CognitiveService;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class Benchmarks : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;

        public IConfigurationRoot Configuration { get; }

        private string _apibaseUrl;


        //All services
        private BenchmarkService _svcBenchmark;
        private AuthService _svcAuth;
        private MessageService _svcMsg;
        private PlayerResponseService _svcPlayerResponse;
        private PracticeService _svcPractice;
        private QuestionnaireService _svcQuestionnaire;
        private SessionService _svcSession;
        private CognitiveService _svcCognitive;
        private DrillService _svcDrill;
        private SettingsService _svcSettings;
        private TeamService _svcTeam;
        private WorkoutService _svcWorkout;

        public Benchmarks(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            var builder =
                new ConfigurationBuilder()
                    .AddJsonFile("testsettings.json")
                    .AddEnvironmentVariables();
            Configuration = builder.Build();

            this._apibaseUrl = Configuration["AAD:Url"];

            //Create the all services
            _svcBenchmark = new BenchmarkService(_fixture.Context);
            _svcAuth = new AuthService(_fixture.Context);
            _svcMsg = new MessageService(_fixture.Context);
            _svcPlayerResponse = new PlayerResponseService(_fixture.Context);
            _svcPractice = new PracticeService(_fixture.Context);
            _svcQuestionnaire = new QuestionnaireService(_fixture.Context);
            _svcSession = new SessionService(_fixture.Context);
            _svcCognitive = new CognitiveService(_fixture.Context);
            _svcDrill = new DrillService(_fixture.Context);
            _svcSettings = new SettingsService(_fixture.Context);
            _svcTeam = new TeamService(_fixture.Context);
            _svcWorkout = new WorkoutService(_fixture.Context);

        }


        [Fact]
        public async void BenchmarkAll()
        {
            int numOfRuns = Int32.Parse(Configuration["Benchmark:NumberOfRuns"]);

            for (int i = 0; i < numOfRuns; i++)
            {
                await BenchmarksAuth();
                await BenchmarksPractice();
                await BenchmarksQuestionnaire();
            }
        }


        [Fact]
        public async Task BenchmarksAuth()
        {
            try
            {

                //Auth Insert
                var random = new Random();
                var ind = random.Next(1, 1000);
                var newUser = new UserDto()
                {
                    FirstName = $"TestFName#{ind}",
                    LastName = $"TestLName#{ind}",
                    FullName = $"TestFullName#{ind}",
                    Height = 170,
                    isEnabled = true,
                    isActive = true,
                    TeamId = ind,
                    Email = $"testuser{ind}@tppusers.onmicrosoft.com",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddYears(1)
                };

                var apiCallUrl = $"{this._apibaseUrl}/api/v1/auth/user";
                var startTime = DateTime.UtcNow;
                var result = await PostItemAsync<UserDto>(apiCallUrl, newUser);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Auth",
                        Method = "CreateUser",
                        Operation = "Insert",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });

                    //Clean the test data
                    var newUserId = await result.Content.ReadAsStringAsync();
                    await _svcAuth.DeleteUser(Int32.Parse(newUserId));
                }



                //Authenticate user
                var aadId = "93621dc3-43c1-4ade-adaa-9e8955931772";
                apiCallUrl = $"{this._apibaseUrl}/api/v1/auth/{aadId}";
                startTime = DateTime.UtcNow;
                var teamId = await GetItemAsync<int>(apiCallUrl);
                if (teamId > 0)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Auth",
                        Method = "AuthenticateUser",
                        Operation = "Join Select",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Retrieve
                int userId = 9780;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/auth/user/{userId}";
                startTime = DateTime.UtcNow;
                var userDto = await GetItemAsync<UserDto>(apiCallUrl);
                if (userDto != null)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Auth",
                        Method = "GetUser",
                        Operation = "Select by Id",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Update user
                ind = random.Next(1, 1000);
                var user = await _svcAuth.GetUser(userId);
                user.MiddleName = $"MiddleName#{ind}";
                user.TeamId = 1;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/auth/user/update";
                startTime = DateTime.UtcNow;
                result = await PostItemAsync<UserDto>(apiCallUrl, user);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Auth",
                        Method = "UpdateUser",
                        Operation = "Update",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        [Fact]
        public async Task BenchmarksPractice()
        {
            try
            {

                //Insert
                var random = new Random();
                var ind = random.Next(1, 1000);
                var practice = new PracticeDto()
                {
                    Coach = null,
                    EstimatedTrainingLoad = 525,
                    IsModified = false,
                    Name = $"Test Practice #{ind}",
                    Note = null,
                    RecommendedTrainingLoad = 375,
                    Session = null,
                    SessionId = 1,
                    Side = null,
                    SubTopic = "Test Subtopic",
                    TeamId = 1,
                    Topic = "Test Topic",
                    PracticeDrills = new List<PracticeDrillDto>() {new PracticeDrillDto()
                    {
                        CalculatedTrainingLoad = 175,
                        Drill = null,
                        DrillId = 2,
                        Duration = 30,
                        IsModified = false,
                        Note = null,
                        NoteId = 0,
                        NumberOfPlayers = 10,
                        Practice = null,
                        PracticeId = 0,
                        Sequence = 1,
                        Size = "60x60"
                    }, new PracticeDrillDto()
                        {
                            CalculatedTrainingLoad = 200,
                            Drill = null,
                            DrillId = 4,
                            Duration = 30,
                            IsModified = false,
                            Note = null,
                            NoteId = 0,
                            NumberOfPlayers = 7,
                            Practice = null,
                            PracticeId = 0,
                            Sequence = 2,
                            Size = "18x18"
                        }}
                };

                var apiCallUrl = $"{this._apibaseUrl}/api/v1/practices";
                var startTime = DateTime.UtcNow;
                var result = await PostItemAsync<PracticeDto>(apiCallUrl, practice);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Practice",
                        Method = "CreatePractice",
                        Operation = "Insert",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });

                    //Clean the test data
                    var practiceId = await result.Content.ReadAsStringAsync();
                    await _svcPractice.DeletePractice(Int32.Parse(practiceId));

                }



                //Retrieve all practices
                apiCallUrl = $"{this._apibaseUrl}/api/v1/practices";
                startTime = DateTime.UtcNow;
                var practices = await GetItemAsync<List<PracticeDto>>(apiCallUrl);
                if (practices != null)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Practice",
                        Method = "GetAllPractices",
                        Operation = "Select All",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Retrieve the practice
                int id = 1;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/practices/id/{id}";
                startTime = DateTime.UtcNow;
                var dtoPractice = await GetItemAsync<PracticeDto>(apiCallUrl);
                if (dtoPractice != null)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Practice",
                        Method = "GetPractice",
                        Operation = "Select by Id",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Update practice
                ind = random.Next(1, 1000);
                dtoPractice = dtoPractice ?? await _svcPractice.GetPractice(1);
                dtoPractice.IsModified = true;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/practices/update";
                startTime = DateTime.UtcNow;
                result = await PostItemAsync<PracticeDto>(apiCallUrl, dtoPractice);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Practice",
                        Method = "UpdatePractice",
                        Operation = "Update",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        [Fact]
        public async Task BenchmarksQuestionnaire()
        {
            try
            {

                //Insert
                var random = new Random();
                var ind = random.Next(1, 1000);
                var questionnaire = new AthleteQuestionnaireDto()
                {
                    IsEnabled = true,
                    Name = $"Test Questionnaire Name #{ind}",
                    SessionId = 1,
                    SequenceOrder = 1,
                    StartDateTime = DateTime.UtcNow,
                    EndDateTime = DateTime.UtcNow.AddYears(1),
                    Questions = new List<AthleteQuestionDto>()
                    {
                        new AthleteQuestionDto()
                        {
                            Text = $"Test Question #{ind}",
                            SequenceOrder = 1,
                            MinCaptionValue = new KeyValuePair<string, int>("Low", 0),
                            MidCaptionValue = new KeyValuePair<string, int>("Mid", 5),
                            MaxCaptionValue = new KeyValuePair<string, int>("Max", 10),
                        },
                        new AthleteQuestionDto()
                        {
                            Text = $"Test Question #{ind}",
                            SequenceOrder = 2,
                            MinCaptionValue = new KeyValuePair<string, int>("Sad", 0),
                            MidCaptionValue = new KeyValuePair<string, int>("Normal", 5),
                            MaxCaptionValue = new KeyValuePair<string, int>("Happy", 10),
                        }
                    }
                };


                var apiCallUrl = $"{this._apibaseUrl}/api/v1/questionnaires";
                var startTime = DateTime.UtcNow;
                var result = await PostItemAsync<AthleteQuestionnaireDto>(apiCallUrl, questionnaire);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Questionnaire",
                        Method = "CreateQuestionnaire",
                        Operation = "Insert",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });

                    //Clean the test data
                    var questId = await result.Content.ReadAsStringAsync();
                    await _svcQuestionnaire.DeleteQuestionnaire(Int32.Parse(questId));

                }



                //Retrieve all session's questionnaires
                var sessionId = 1;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/questionnaires/list/{sessionId}";
                startTime = DateTime.UtcNow;
                var questionnaires = await GetItemAsync<List<AthleteQuestionnaireDto>>(apiCallUrl);
                if (questionnaires != null)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Questionnaire",
                        Method = "GetSessionQuestionnaires",
                        Operation = "Join Select",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Retrieve the practice
                int id = 1;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/questionnaires/entity/{id}";
                startTime = DateTime.UtcNow;
                var dtoQuest = await GetItemAsync<AthleteQuestionnaireDto>(apiCallUrl);
                if (dtoQuest != null)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Questionnaire",
                        Method = "GetQuestionnaire",
                        Operation = "Select by Id",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }


                //Update questionnaire
                ind = random.Next(1, 1000);
                dtoQuest = dtoQuest ?? await _svcQuestionnaire.GetQuestionnaire(1);
                dtoQuest.IsEnabled = true;
                apiCallUrl = $"{this._apibaseUrl}/api/v1/questionnaires/update";
                startTime = DateTime.UtcNow;
                result = await PostItemAsync<AthleteQuestionnaireDto>(apiCallUrl, dtoQuest);
                if (result.IsSuccessStatusCode)
                {
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = (endTime - startTime).Milliseconds;

                    //Write the benchmark data
                    await _svcBenchmark.AddBenchmark(new Benchmark
                    {
                        Controller = "Questionnaire",
                        Method = "UpdateQuestionnaire",
                        Operation = "Update",
                        Time = elapsedTime,
                        LastRun = DateTime.UtcNow
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private async Task<string> GetAuthToken()
        {
            var authority = string.Format(CultureInfo.InvariantCulture, Configuration["AAD:Instance"], Configuration["AAD:Tenant"]);
            var authContext = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var clientId = Configuration["AAD:ClientID"];
            var clientKey = Configuration["AAD:ClientKey"];
            var credentials = new ClientCredential(clientId, clientKey);
            var audience = Configuration["AAD:Audience"];
            var authResult = await authContext.AcquireTokenAsync(audience, credentials);
            return authResult?.AccessToken;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> PostItemAsync<T>(string uri, T item)
        {
            var hndlr = new HttpClientHandler { UseDefaultCredentials = true };

            try
            {
                using (var client = new HttpClient(hndlr))
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var token = await GetAuthToken();
                    if (token != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, jsonContent);

                    response.EnsureSuccessStatusCode();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<T> GetItemAsync<T>(string uri)
        {
            var hndlr = new HttpClientHandler { UseDefaultCredentials = true };

            try
            {
                using (var client = new HttpClient(hndlr))
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var token = await GetAuthToken();
                    if (token != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.GetAsync(uri);

                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}