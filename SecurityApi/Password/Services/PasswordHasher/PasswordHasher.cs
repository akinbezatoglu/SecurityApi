using System.Security.Cryptography;

namespace SecurityApi.Password.Services.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
        private readonly string Seperator = ".";

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}{Seperator}{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string hashedPassword)
        {
            try
            {
                string[] parts = hashedPassword.Split(Seperator);
                byte[] hash = Convert.FromHexString(parts[0]);
                byte[] salt = Convert.FromHexString(parts[1]);

                byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
                return CryptographicOperations.FixedTimeEquals(hash, inputHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
