using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class SignInViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
