using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace secure_api.Services;

public class PasswordHasher
{
    public static string GenerateSalt()
    {
        byte[] salt = new byte[16];

        // A ideia era utilizar o RNGCryptoServiceProvider, mas parece estar obsoleto.
        // Um minuto de silêncio para essa relíquia.
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt); 
        }

        return Convert.ToBase64String(salt);
    }

    public static byte[] GenerateHashedPassword(string password, byte[] salt, byte[] pepper, int iterations)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        // Copia os bytes de passwordBytes, a partir do índice 0, para saltedPassword no índice 0,
        // passwordBytes.Length é o número de bytes a serem copiados.
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);
        // Copia os bytes de salt, a partir do índice 0, para saltedPassword a partir do índice
        // que corresponde a passwordBytes.Length, salt.Length é o número de bytes a serem copiados.

        using var pbkdf2 = new Rfc2898DeriveBytes(saltedPassword, pepper, iterations);
        byte[] passwordHash = pbkdf2.GetBytes(32); // Tamanho do hash em bytes, 32 x 8 = 256 bits

        return passwordHash;
    }

    public static bool VerifyPassword(string password, string savedHashedPassword, string savedSalt, string savedPepper, int iterations)
    {
        byte[] salt = Encoding.UTF8.GetBytes(savedSalt);
        byte[] pepper = Encoding.UTF8.GetBytes(savedPepper);
        byte[] hashedPasswordToCheck = GenerateHashedPassword(password, salt, pepper, iterations);
        string hashedPasswordToCheckBase64 = Convert.ToBase64String(hashedPasswordToCheck);


        return hashedPasswordToCheckBase64 == savedHashedPassword;
    }
}
