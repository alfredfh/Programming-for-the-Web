using System.ComponentModel.DataAnnotations;

namespace VoteForCauseFinal.Models.ViewModels
{
    public class RegisterViewModel
    {
        //validation
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password at least 6 charachter")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
