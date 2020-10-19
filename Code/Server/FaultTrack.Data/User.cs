namespace FaultTrack.Data
{
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128, MinimumLength=128)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 128)]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public string NormalizedUserName { get; set; }
    }
}