// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    public class AppCredentialsDTO
    {
        public string AADInstance { get; set; }
        public string Tenant { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientKey { get; set; }
        public string Token { get; set; }
    }
}