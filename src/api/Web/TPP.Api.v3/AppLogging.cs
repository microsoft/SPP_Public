// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TPP.Api
{
    public static class AppLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory()
            .AddConsole()
            .AddDebug()
            .AddSerilog();
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();

    }
}