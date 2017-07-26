// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TPP.Core.Data;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace TPP.UnitTests
{
    public class DataInitFixture : IDisposable
    {
        public BaseDbContext Context { get; }
        public IConfigurationRoot Configuration { get; }

        public DataInitFixture()
        {
            var builder =
                new ConfigurationBuilder()
                    .AddJsonFile("testsettings.json")
                    .AddEnvironmentVariables();
            Configuration = builder.Build();

            //Get SQL connection string
#if LOCALDB
            var dbConnection = new SqlConnection(Configuration["ConnectionStrings:TppLocalDbConnection"]);
#else
            var dbConnection = new SqlConnection(Configuration["ConnectionStrings:TppDbConnection"]);
#endif
            //Initialize DbContext in memory
            var optionBuiler = new DbContextOptionsBuilder();
            optionBuiler.UseInMemoryDatabase();
            Context = new BaseDbContext(optionBuiler.Options, dbConnection);

            // Do "global" initialization here; Only called once.
            //InsertTestData();

        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }


    }


    [CollectionDefinition("TPPDatabase")]
    public class TPPDataCollection : ICollectionFixture<DataInitFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}