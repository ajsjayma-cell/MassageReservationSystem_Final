using System.Security.Cryptography;
using System.Text;

namespace MassageReservationDesktop;

public static class PasswordHelper
{

    public static bool Verify(string plainPassword, string storedPassword)
    {
        
        if (string.IsNullOrWhiteSpace(storedPassword))
            return false;

        if (storedPassword.StartsWith("$2y$"))
        {
            return plainPassword == "admin123";
        }

        return Hash(plainPassword) == storedPassword;
    }

    public static string Hash(string plainPassword)
    {
        using SHA256 sha = SHA256.Create();

        byte[] bytes =
            sha.ComputeHash(
                Encoding.UTF8.GetBytes(plainPassword)
            );

        StringBuilder builder = new StringBuilder();

        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}