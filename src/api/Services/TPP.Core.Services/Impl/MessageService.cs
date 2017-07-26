// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

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
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageDto, Message>();
            RecognizeDestinationPrefixes("Message");
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderFirstName, opt => opt.MapFrom(src => src.Sender.FirstName))
                .ForMember(dest => dest.SenderLastName, opt => opt.MapFrom(src => src.Sender.LastName))
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.PathtoPhoto))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.Sent))
                ;

            CreateMap<User, UserDto>();
        }
    }

    public class MessageService : TPPDbService, IMessageService
    {
        private IDbConnection _db;

        public MessageService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }


        public MessageService(BaseDbContext context) : base(context)
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


        public async Task<List<MessageDto>> GetMessages(int playerId)
        {
            var messages = await _db.FindAsync<Message>(query => query
                .Where($"{nameof(Message.ToId):C} = @PlayerId and {nameof(Message.IsActive):C} = @IsActive")
                .Include<User>(join => join.InnerJoin())
                .OrderBy($"{nameof(Message.Sent):C} DESC")
                .WithParameters(new
                {
                    PlayerId = playerId,
                    IsActive = true
                }));


            return messages?.Select(messageDb => Mapper.Map<MessageDto>(messageDb)).ToList();

        }

        public async Task<int> CreateMessage(Models.MessageDto messageDto)
        {
            //Map to the Data Entity object
            Message recDb = Mapper.Map<Message>(messageDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateMessage(Models.MessageDto messageDto)
        {
            //Map the DTO object into the data entity
            var recDb = Mapper.Map<Message>(messageDto);
            return await _db.UpdateAsync(recDb);
        }

        public async Task<bool> DeleteMessage(int messageId)
        {
            //Find the the record to be deleted
            var rec = new Message() { Id = messageId };
            var recDb = await _db.GetAsync(rec);

            //Delete the the record from the DB
            return await _db.DeleteAsync(recDb);
        }

        public async Task<bool> DeletePlayerMessages(int playerId)
        {
            bool result = true;

            //Find all message records belonging to this player
            var recs = await _db.FindAsync<Message>(query => query
                .Where($"{nameof(Message.ToId):C} = @playerId")
                .WithParameters(new { playerId }));
            foreach (var rec in recs)
            {
                result &= await _db.DeleteAsync(rec);
            }

            return result;
        }

        public async Task<Models.MessageDto> GetMessage(int messageId)
        {
            //Find the the record
            var msgRec = new Message() { Id = messageId };
            var recDb = await _db.GetAsync(msgRec);

            //Map to the DTO table
            return Mapper.Map<MessageDto>(recDb);
        }

    }



}