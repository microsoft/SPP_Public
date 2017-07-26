// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IAuthService : IEntityConfiguration
    {
        Task<int> AuthenticateUser(string userName);

        Task<int> CreateUser(UserDto userDto);

        Task<UserDto> GetUser(int userId);

        Task<bool> UpdateUser(UserDto userDto);

        Task<int> DeleteUser(int userId);

        Task<AppCredentialsDTO> GetToken(AppCredentialsDTO appCredentials);
    }
}