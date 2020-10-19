// -----------------------------------------------------------------------------
//  <copyright file="RoleStore.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Entity Framework store that manages user roles.
    /// </summary>
    public class RoleStore : IRoleStore<UserRole>
    {
        /// <inheritdoc />
        public void Dispose()
        {
        }
 
        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        } 
    }
}