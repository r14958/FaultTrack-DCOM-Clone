namespace FaultTrack.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserToken
    {
        public int UserTokenId { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(128, MinimumLength=128)]
        public string Token { get; set; }

        public virtual User User { get; set; }
    }
}