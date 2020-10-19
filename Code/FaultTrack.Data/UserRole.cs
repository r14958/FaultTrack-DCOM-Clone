// -----------------------------------------------------------------------------
//  <copyright file="UserRole.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(UserRole))]
    public class UserRole
    {
        [Key, Required]
        public virtual int UserRoleId { get; set; }

        [Required, ForeignKey(nameof(User))]
        public virtual int UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public virtual int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}