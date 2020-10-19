// -----------------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Web
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Entity Framework store that manages user accounts.
    /// </summary>
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private readonly DataContext db;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStor"/> class.
        /// </summary>
        /// <param name="db"></param>
        public UserStore(DataContext db)
        {
            this.db = db;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db?.Dispose();
            }
        }

        /// <inheritdoc />
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }
 
        /// <inheritdoc />
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }
 
        /// <inheritdoc />
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
 
        /// <inheritdoc />
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object) null);
        }
 
        /// <inheritdoc />
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            db.Add(user);
 
            await db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(IdentityResult.Success);
        }
 
        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
 
        /// <inheritdoc />
        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            db.Remove(user);
             
            int i = await db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }
 
        /// <inheritdoc />
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
 
        /// <inheritdoc />
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await db.Users.SingleOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName, cancellationToken);
        }
 
        /// <inheritdoc />
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
 
            return Task.FromResult((object) null);
        }
 
        /// <inheritdoc />
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }
 
        /// <inheritdoc />
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }
    }
}