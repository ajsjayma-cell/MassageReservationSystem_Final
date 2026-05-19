using System.Security.Cryptography;
using System.Text;

namespace MassageReservationDesktop
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            using var sha = SHA256.Create();

            byte[] bytes = sha.ComputeHash(
                Encoding.UTF8.GetBytes(password)
            );

            StringBuilder builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(
                    b.ToString("x2")
                );
            }

            return builder.ToString();
        }

        internal static bool Verify(string text, string v)
        {
            throw new NotImplementedException();
        }

    }
}