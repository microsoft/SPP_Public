// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPP.Core.Data.Entities;
using Type = System.Type;


namespace TPP.Core.Data
{
    public sealed class TPPContext : BaseDbContext
    {
        public IDbConnection TppDbConnection { get; }

        public TPPContext(DbContextOptions<TPPContext> options) : base(options)
        {
            TppDbConnection = this.Database.GetDbConnection();
        }


    }
}