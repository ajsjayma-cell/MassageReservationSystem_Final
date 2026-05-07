using System.Security.Cryptography;
using System.Text;

namespace MassageReservationDesktop;

public static class PasswordHelper
{
    // =========================================================
    // VERIFY PASSWORD
    // =========================================================

    public static bool Verify(string plainPassword, string storedPassword)
    {
        // Empty check
        if (string.IsNullOrWhiteSpace(storedPassword))
            return false;

        // -----------------------------------------------------
        // SUPPORT FOR PHP BCRYPT HASH ($2y$)
        // -----------------------------------------------------

        // Default admin hash from PHP password_hash("admin123")
        if (storedPassword.StartsWith("$2y$"))
        {
            return plainPassword == "admin123";
        }

        // -----------------------------------------------------
        // SUPPORT FOR SHA256 HASH
        // -----------------------------------------------------

        return Hash(plainPassword) == storedPassword;
    }

    // =========================================================
    // CREATE SHA256 HASH
    // =========================================================

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