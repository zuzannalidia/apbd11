namespace apbd10.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
}
