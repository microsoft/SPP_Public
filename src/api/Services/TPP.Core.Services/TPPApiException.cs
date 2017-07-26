// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Impl
{
    public class TPPApiException : Exception
    {
        private Exception ex;

        public TPPApiException()
        {
        }

        public TPPApiException(string message) : base(message)
        {
        }

        public TPPApiException(Exception ex)
        {
            this.ex = ex;
        }

        public TPPApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}