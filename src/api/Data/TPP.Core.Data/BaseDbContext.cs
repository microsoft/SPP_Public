// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Data;
using Microsoft.EntityFrameworkCore;
using TPP.Core.Data.Entities;

namespace TPP.Core.Data
{
    public class BaseDbContext : DbContext
    {
        public IDbConnection DbConnection { get; }

        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public BaseDbContext(DbContextOptions options, IDbConnection dbConnection)
            : base(options)
        {
            DbConnection = dbConnection;
        }

        public virtual DbSet<Catapult> Catapults { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<CognitiveService> CognitiveServices { get; set; }
        public virtual DbSet<DayType> DayTypes { get; set; }
        public virtual DbSet<Drill> Drills { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Hydration> Hydrations { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<InjuryData> InjuryDatas { get; set; }
        public virtual DbSet<Locale> Locales { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MatchDayType> MatchDayTypes { get; set; }
        public virtual DbSet<Mesocycle> Mesocycles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MetricSet> MetricSets { get; set; }
        public virtual DbSet<MetricsFor> MetricsFors { get; set; }
        public virtual DbSet<Microcycle> Microcycles { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerMeasurementType> PlayerMeasurementTypes { get; set; }
        public virtual DbSet<PlayerMedicalMeasurement> PlayerMedicalMeasurements { get; set; }
        public virtual DbSet<PlayerMetric> PlayerMetrics { get; set; }
        public virtual DbSet<PlayerMetricValue> PlayerMetricValues { get; set; }
        public virtual DbSet<PlayerRadarMetric> PlayerRadarMetrics { get; set; }
        public virtual DbSet<PlayerRating> PlayerRatings { get; set; }
        public virtual DbSet<PlayerReadiness> PlayerReadinesses { get; set; }
        public virtual DbSet<PlayerRestriction> PlayerRestrictions { get; set; }
        public virtual DbSet<PlayerRPE> PlayerRPEs { get; set; }
        public virtual DbSet<PlayerSession> PlayerSessions { get; set; }
        public virtual DbSet<PlayerSquad> PlayerSquads { get; set; }
        public virtual DbSet<PlayerTimeTrial> PlayerTimeTrials { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<PracticeData> PracticeDatas { get; set; }
        public virtual DbSet<PracticeDrill> PracticeDrills { get; set; }
        public virtual DbSet<PracticeDrillData> PracticeDrillDatas { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
        public virtual DbSet<QuestionnaireResponse> QuestionnaireResponses { get; set; }
        public virtual DbSet<QuestionnaireStatus> QuestionnaireStatus { get; set; }
        public virtual DbSet<Readiness> Readinesses { get; set; }
		public virtual DbSet<Restriction> Restrictions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<SeasonSubTeam> SeasonSubTeams { get; set; }
        public virtual DbSet<SeattleFlamesHydration2_Data> SeattleFlamesHydration2_Data { get; set; }
        public virtual DbSet<SeattleFlamesHydration2_Schema> SeattleFlamesHydration2_Schema { get; set; }
        public virtual DbSet<SeattleFlames_Catapult> SeattleFlames_Catapult { get; set; }
        public virtual DbSet<SeattleFlames_Readiness> SeattleFlames_Readiness { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionUser> SessionUsers { get; set; }
        public virtual DbSet<Soreness> Sorenesses { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<SubPosition> SubPositions { get; set; }
        public virtual DbSet<SubTeam> SubTeams { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMatch> TeamMatches { get; set; }
        public virtual DbSet<TeamReadiness> TeamReadinesses { get; set; }
        public virtual DbSet<TrainingLoad> TrainingLoads { get; set; }
        public virtual DbSet<SessionType> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserEmotion> UserEmotions { get; set; }
        public virtual DbSet<UserQuestionnaire> UserQuestionnaires { get; set; }
        public virtual DbSet<UserTeam> UserTeams { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutData> WorkoutDatas { get; set; }
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public virtual DbSet<WorkoutExerciseData> WorkoutExerciseDatas { get; set; }
        public virtual DbSet<TeamImages> TeamImages { get; set; }
        public virtual DbSet<ADB2CSettings> ADB2CSettings { get; set; }
        public virtual DbSet<Benchmark> Benchmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SubTeam>()
                .Property(e => e.Gender)
                .HasMaxLength(10);


            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .HasMaxLength(10);

        }

    }
}