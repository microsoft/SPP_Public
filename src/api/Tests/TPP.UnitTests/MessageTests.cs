// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class MessageTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private MessageService _service;

        public MessageTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new MessageService(_fixture.Context);

        }




        [Fact]
        public async void CreateMessage()
        {
            var newMessage = new MessageDto()
            {
                MessageText = "New MessageDto",
                SenderFirstName = "First",
                SenderLastName = "CoachDto"
            };

            var result = await _service.CreateMessage(newMessage);
            Assert.NotEqual(0, result);
        }


        [Theory]
        [InlineData(3)]
        public async void GetMessages(int playerId)
        {
            var messages = await _service.GetMessages(playerId);
            Assert.NotNull(messages);
        }



        [Theory]
        [InlineData(1)]
        public async void GetMessage(int messageId)
        {
            var message = await _service.GetMessage(messageId);
            Assert.NotNull(message);
        }


        [Theory]
        [InlineData(100000)]
        public async void GetNonExistingMessage(int messageId)
        {
            var message = await _service.GetMessage(messageId);
            Assert.Null(message);
        }


        [Theory]
        [InlineData(4)]
        public async void UpdateMessage(int messageId)
        {
            var message = new MessageDto()
            {
                MessageId = messageId,
                MessageText = "Updated Text"
            };

            var result = await _service.UpdateMessage(message);
            Assert.Equal(true, result);
        }


        [Theory]
        [InlineData(4)]
        public async void DeleteMessage(int messageId)
        {
            var result = await _service.DeleteMessage(messageId);
            Assert.Equal(true, result);
        }

    }
}