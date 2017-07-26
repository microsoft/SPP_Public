// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.FastCrud;
using TPP.Core.Data;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Contracts;
using TPP.Core.Services.Models;


namespace TPP.Core.Services.Impl
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<AthleteQuestionnaireDto, Questionnaire>()
                .ForMember(dest => dest.DayTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.MesocycleId, opt => opt.Ignore())
                .ForMember(dest => dest.MicrocycleId, opt => opt.Ignore());

             CreateMap<Questionnaire, AthleteQuestionnaireDto>()
                .ForMember(dest => dest.Questions, opt => opt.Ignore());

            CreateMap<AthleteQuestionDto, Question>()
                .ForMember(dest => dest.MaxCaption, opt => opt.MapFrom(src => src.MaxCaptionValue.Key))
                .ForMember(dest => dest.MaxValue, opt => opt.MapFrom(src => src.MaxCaptionValue.Value))
                .ForMember(dest => dest.MidCaption, opt => opt.MapFrom(src => src.MidCaptionValue.Key))
                .ForMember(dest => dest.MidValue, opt => opt.MapFrom(src => src.MidCaptionValue.Value))
                .ForMember(dest => dest.MinCaption, opt => opt.MapFrom(src => src.MinCaptionValue.Key))
                .ForMember(dest => dest.MinValue, opt => opt.MapFrom(src => src.MinCaptionValue.Value))
                ;

            CreateMap<QuestionnaireQuestion, AthleteQuestionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Question.Text ))
                .ForMember(dest => dest.MaxCaptionValue, opt => opt.MapFrom(src => 
                    new KeyValuePair<string, int>(src.Question.MaxCaption, (int)src.Question.MaxValue)))
                .ForMember(dest => dest.MidCaptionValue, opt => opt.MapFrom(src => 
                    new KeyValuePair<string, int>(src.Question.MidCaption, (int)src.Question.MidValue)))
                .ForMember(dest => dest.MinCaptionValue, opt => opt.MapFrom(src => 
                    new KeyValuePair<string, int>(src.Question.MinCaption, (int)src.Question.MinValue)))
                .ForMember(dest => dest.SequenceOrder, opt => opt.MapFrom(src => src.Question.SequenceOrder))
                ;

            CreateMap<Question, AthleteQuestionDto>()
                .ForMember(dest => dest.MaxCaptionValue, opt => opt.MapFrom(src =>
                    new KeyValuePair<string, int>(src.MaxCaption, (int)src.MaxValue)))
                .ForMember(dest => dest.MidCaptionValue, opt => opt.MapFrom(src =>
                    new KeyValuePair<string, int>(src.MidCaption, (int)src.MidValue)))
                .ForMember(dest => dest.MinCaptionValue, opt => opt.MapFrom(src =>
                    new KeyValuePair<string, int>(src.MinCaption, (int)src.MinValue)))
                ;

            CreateMap<QuestionnaireResponse, QuestionResponseDto>()
                .ForMember(dest => dest.Answer,
                    opt => opt.MapFrom(src => new KeyValuePair<string, int>(src.QuestionId.ToString(), (int) src.Value)))
                ;
        }
    }


    public class QuestionnaireService : TPPDbService, IQuestionnaireService
    {
        private IDbConnection _db;

        public QuestionnaireService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public QuestionnaireService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
            RegisterEntities();
        }

        public void RegisterEntities()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;

            //Initialize static mapping
            AutoMapperConfiguration.Configure();
        }


        #region CREATE
        public async Task<int> CreateQuestionnaire(AthleteQuestionnaireDto questionnaireDto)
        {
            //Map to the Data Entity object
            var questionnaireDb = Mapper.Map<Questionnaire>(questionnaireDto);

            //1. Insert into Questionnaire table
            await _db.InsertAsync(questionnaireDb);

            if (questionnaireDb.Id <= 0) return 0;

            //2. Map to the Question entity and save into the DB
            foreach (var dtoQuestion in questionnaireDto.Questions)
            {
                var question = Mapper.Map<Question>(dtoQuestion);

                //2. Write to the DB
                await _db.InsertAsync(question);

                //3. Create relationship between the 2 tables
                var questJoin = new QuestionnaireQuestion()
                {
                    QuestionnaireId = questionnaireDb.Id,
                    QuestionId = question.Id
                };
                await _db.InsertAsync(questJoin);
            }


            return questionnaireDb.Id;
        }
        #endregion


        #region RETRIEVE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<AthleteQuestionnaireDto> GetAthleteQuestions(int sessionId)
        {
            //Get the player's questionnaire
            var questionnaireDb = await _db.FindAsync<QuestionnaireQuestion>(query => query
                .Include<Questionnaire>(join => join.InnerJoin()
                .Where($"{nameof(Questionnaire.SessionId):C} = @sessionId and @today Between {nameof(Questionnaire.StartDateTime):C} and {nameof(Questionnaire.EndDateTime):C}")
                .OrderBy($"{nameof(Questionnaire.StartDateTime):C} DESC"))
                .Include<Question>(join => join.InnerJoin())
                .WithParameters(new
                {
                    sessionId = sessionId,
                    today = DateTime.UtcNow
                }));


            foreach (var questionnaire in questionnaireDb.GroupBy(x => x.QuestionnaireId))
            {
                //Get the parent questionnaire object
                var questionQuestionnaire =
                    await _db.GetAsync<Questionnaire>(new Questionnaire() { Id = questionnaire.First().QuestionnaireId });

                var athleteQuestionnaire = Mapper.Map<AthleteQuestionnaireDto>(questionQuestionnaire);
                if (athleteQuestionnaire == null) continue;
                athleteQuestionnaire.Questions = new List<AthleteQuestionDto>();

                foreach (var questionnaireQuestion in questionnaire)
                {
                    var atQ = Mapper.Map<AthleteQuestionDto>(questionnaireQuestion.Question);
                    athleteQuestionnaire.Questions.Add(atQ);
                }

                if(athleteQuestionnaire.Questions.Count > 0)
                    return athleteQuestionnaire;
            }


            return null;
        }


        public async Task<List<AthleteQuestionnaireDto>> GetSessionQuestionnaires(int sessionId)
        {
            List<AthleteQuestionnaireDto> questionnaireList = new List<AthleteQuestionnaireDto>();

            //Get the player's questionnaire
            var questionnaireDb = await _db.FindAsync<QuestionnaireQuestion>(query => query
                .Include<Questionnaire>(join => join.InnerJoin()
                .Where($"{nameof(Questionnaire.SessionId):C} = @sessionId and @today Between {nameof(Questionnaire.StartDateTime):C} and {nameof(Questionnaire.EndDateTime):C}")
                .OrderBy($"{nameof(Questionnaire.StartDateTime):C} DESC"))
                .Include<Question>(join => join.InnerJoin())
                .WithParameters(new
                {
                    sessionId = sessionId,
                    today = DateTime.UtcNow
                }));


            foreach (var questionnaire in questionnaireDb.GroupBy(x => x.QuestionnaireId))
            {
                //Get the parent questionnaire object
                var questionQuestionnaire =
                    await _db.GetAsync<Questionnaire>(new Questionnaire() {Id = questionnaire.First().QuestionnaireId});

                var athleteQuestionnaire = Mapper.Map<AthleteQuestionnaireDto>(questionQuestionnaire);
                if (athleteQuestionnaire == null) continue;
                athleteQuestionnaire.Questions = new List<AthleteQuestionDto>();

                foreach (var questionnaireQuestion in questionnaire)
                {
                    var atQ = Mapper.Map<AthleteQuestionDto>(questionnaireQuestion.Question);
                    athleteQuestionnaire.Questions.Add(atQ);
                }

                questionnaireList.Add(athleteQuestionnaire);
            }


            return questionnaireList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<List<AthleteQuestionHistoryDto>> GetPlayerResponse(int sessionId, int playerId)
        {
            var questionHistory = new List<AthleteQuestionHistoryDto>();

            //Get the player's responses
            var responses = await _db.FindAsync<QuestionnaireResponse>(query => query
                .Where($"{nameof(QuestionnaireResponse.UserId):C} = @playerId")
                .Include<Questionnaire>(join => join.InnerJoin()
                .Where($"{nameof(Questionnaire.SessionId):C} = @sessionId"))
                .Include<Question>(join => join.InnerJoin())
                .WithParameters(new { sessionId, playerId }));


            foreach (var response in responses)
            {
                //Get the player's questions for each response
                //var question = await _db.GetAsync(new Question() {Id = (int) response.QuestionId});

                questionHistory.Add(new AthleteQuestionHistoryDto()
                {
                    Question = Mapper.Map<AthleteQuestionDto>(response.Question),
                    Responses = new List<QuestionResponseDto>()
                    {
                        Mapper.Map<QuestionResponseDto>(response)
                    }
                });
            }


            return questionHistory;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        public async Task<AthleteQuestionnaireDto> GetQuestionnaire(int questionnaireId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Questionnaire() { Id = questionnaireId });

            return Mapper.Map<AthleteQuestionnaireDto>(dbRec);
        }




        #endregion

        #region UPDATE

        public async Task<bool> UpdateQuestionnaire(AthleteQuestionnaireDto questionnaireDto)
        {
            //Map to the DB object
            var newRec = Mapper.Map<Questionnaire>(questionnaireDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        #endregion


        #region DELETE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteQuestionnaire(int questionnaireId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Questionnaire() { Id = questionnaireId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);

        }

        #endregion

    }
}