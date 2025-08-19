using System.Security.Cryptography;
using System.Text;

namespace valor_chain.api.Application.Security
{

    public static class HashHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Créer une instance de SHA256
            using (var sha256Hash = SHA256.Create())
            {
                // Convertir la chaîne en tableau de bytes
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convertir les bytes en chaîne hexadécimale
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2")); // format hexadécimal
                }
                return builder.ToString();
            }
        }
    }

}
