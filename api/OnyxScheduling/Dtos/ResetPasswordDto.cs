namespace OnyxScheduling.Dtos;

public class ResetPasswordDto
{
    public string UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}