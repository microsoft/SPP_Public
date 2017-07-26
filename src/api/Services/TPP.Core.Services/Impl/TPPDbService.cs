// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using TPP.Core.Data;
using TPP.Core.Services.Contracts;

namespace TPP.Core.Services.Impl
{
    public abstract class TPPDbService
    {
        public BaseDbContext TppContext { get; }

        protected TPPDbService(BaseDbContext context)
        {
            TppContext = context;
        }

        
    }
}