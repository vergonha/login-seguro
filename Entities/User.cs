namespace secure_api.Entities;

// Representa a entidade User e seus atributos.
public sealed class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }


}
