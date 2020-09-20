using System.ComponentModel.DataAnnotations;
namespace Nviromon.Request_Models
{
    public class CreateUserRequestModel
    {
        [Required]
        [EmailAddress]
        public string username { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 24 characters.")]
        public string password { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }


    }
}