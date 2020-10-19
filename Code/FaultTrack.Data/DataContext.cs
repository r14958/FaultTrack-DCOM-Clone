// -----------------------------------------------------------------------------
//  <copyright file="DataContext.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Data
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Data access context for accessing data entities.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">A <see cref="DbContextOptions{DataContext}"/> containing connection information.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="Role"/> entities.
        /// </summary>
        public virtual DbSet<Role> Roles
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the collection that contains <see cref="User"/> entities.
        /// </summary>
        public virtual DbSet<User> Users
        {
            get; 
            set;
        }
        
        /// <summary>
        /// Gets or sets the collection that contains <see cref="UserRole"/> entities.
        /// </summary>
        public virtual DbSet<UserRole> UserRoles
        {
            get;
            set;
        }
    }
}