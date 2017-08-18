// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using AutoMapper;

namespace TPP.Core.Services.Impl
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(ConfigAction);
        }

        public static Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<CognitiveProfile>();
            cfg.AddProfile<MessageProfile>();
            cfg.AddProfile<TeamProfile>();
            cfg.AddProfile<PlayerResponseProfile>();
            cfg.AddProfile<PracticeProfile>();
            cfg.AddProfile<QuestionnaireProfile>();
            cfg.AddProfile<SessionProfile>();
            cfg.AddProfile<WorkoutProfile>();
            cfg.AddProfile<DrillProfile>();
            cfg.AddProfile<SettingsProfile>();
            cfg.AddProfile<LocationProfile>();
        };
    }
}