namespace SEP_Restaurant_management.DTO
{
    public class LoginRequest
    {
        public string UserNameOrEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
