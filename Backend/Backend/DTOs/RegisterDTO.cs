using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class RegisterDTO
    {
        [Required, MinLength(4)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsTeacher { get; set; }
    }
}
