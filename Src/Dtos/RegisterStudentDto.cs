namespace ApiGateway.DTOs
{
    public class RegisterStudentDto
    {
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string RUT { get; set; }
        public string Email { get; set; }
        public int CareerId { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
