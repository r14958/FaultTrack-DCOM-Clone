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

        [Required]
        [StringLength(128, MinimumLength = 128)]
        public string PasswordSalt { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}