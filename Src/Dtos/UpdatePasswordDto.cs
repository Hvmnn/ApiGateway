namespace ApiGateway.DTOs
{
    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
