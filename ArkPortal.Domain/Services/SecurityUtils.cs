using System.Security.Cryptography;
using System.Text;

namespace ArkPotal.Domain.Models.Payments
{
    public interface ISecurityUtils
    {
        string GetSHA512Hash(params string[] parameters);
        string GetSHA512Hash(string stringToHash);
    }

    public class SecurityUtils : ISecurityUtils
    {
        public string GetSHA512Hash(params string[] parameters)
        {
            var stringToHash = string.Concat(parameters);

            return GetSHA512Hash(stringToHash.ToLower());
        }

        public string GetSHA512Hash(string stringToHash)
        {
            using SHA512 alg = new SHA512CryptoServiceProvider();
            byte[] bytes = alg.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}
