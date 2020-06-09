namespace WoWForDummies.Dtos.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}