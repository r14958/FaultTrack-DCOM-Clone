// -----------------------------------------------------------------------------
//  <copyright file="User.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(User))]
    public class User
    {
        [Key, Required]
        public virtual int UserId { get; set; }

        [Required]
        [StringLength(128)]
        public virtual string UserName { get; set; }

        [StringLength(128)]
        public virtual string FirstName { get; set; }

        [StringLength(128)]
        public virtual string LastName { get; set; }

        [StringLength(1)]
        public virtual string MiddleInitial { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 128)]
        public virtual string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string NormalizedEmail { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string NormalizedUserName { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}