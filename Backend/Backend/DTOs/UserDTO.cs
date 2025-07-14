namespace Backend.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTeacher { get; set; }
    }
}
