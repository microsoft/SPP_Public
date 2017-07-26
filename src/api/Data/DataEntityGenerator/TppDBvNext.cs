namespace TPP.Core.Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TppDBvNext : DbContext
    {
        public TppDBvNext()
            : base("name=TppDBvNext")
        {
        }

        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<DayType> DayTypes { get; set; }
        public virtual DbSet<Drill> Drills { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Hydration> Hydrations { get; set; }
        public virtual DbSet<Image> Images { get; set; }
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
        public virtual DbSet<PlayerSession> PlayerSessions { get; set; }
        public virtual DbSet<PlayerSquad> PlayerSquads { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<PracticeDrill> PracticeDrills { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
        public virtual DbSet<QuestionnaireResponse> QuestionnaireResponses { get; set; }
        public virtual DbSet<QuestionnaireStatu> QuestionnaireStatus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<SeasonSubTeam> SeasonSubTeams { get; set; }
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
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserQuestionnaire> UserQuestionnaires { get; set; }
        public virtual DbSet<UserTeam> UserTeams { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutData> WorkoutDatas { get; set; }
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public virtual DbSet<WorkoutExerciseData> WorkoutExerciseDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DayType>()
                .Property(e => e.RecurringDayofWeek)
                .IsUnicode(false);

            modelBuilder.Entity<Drill>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Drill>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Drill>()
                .Property(e => e.SubCategory)
                .IsUnicode(false);

            modelBuilder.Entity<Drill>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Education>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Locale>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mesocycle>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Microcycle>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Nationality>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Availability)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.BowlingGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.BowlingHand)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.BattingGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.BattingHand)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.IsKeeper)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Practice>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Practice>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<Practice>()
                .Property(e => e.SubTopic)
                .IsUnicode(false);

            modelBuilder.Entity<Practice>()
                .Property(e => e.Side)
                .IsUnicode(false);

            modelBuilder.Entity<PracticeDrill>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionnaireResponse>()
                .Property(e => e.Value)
                .HasPrecision(18, 1);

            modelBuilder.Entity<QuestionnaireStatu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Season>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Season>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Sport>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Squad>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Squad>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SubPosition>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SubPosition>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<SubTeam>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<SubTeam>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SubTeam>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Height)
                .HasPrecision(18, 1);

            modelBuilder.Entity<User>()
                .Property(e => e.Weight)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Workout>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Workout>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<Workout>()
                .Property(e => e.SubTopic)
                .IsUnicode(false);

            modelBuilder.Entity<Workout>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<WorkoutExerciseData>()
                .Property(e => e.Weight)
                .HasPrecision(18, 1);
        }
    }
}
