using System.Security.Cryptography;
using System.Text;

namespace Nameless {
    public static class CryptoHelper {
        #region Private Static Read-Only Fields

        private static readonly byte[] Entropy = "baf0b1c9-d96e-4c0c-b4b1-8fd492657593"u8.ToArray();

        #endregion

        #region Public Static Methods

        public static string Encrypt(string value) {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            var buffer = Root.Defaults.Encoding.GetBytes(value);
            var result = ProtectedData.Protect(userData: buffer,
                                               optionalEntropy: Entropy,
                                               scope: DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string value) {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            var buffer = Convert.FromBase64String(value);
            var result = ProtectedData.Unprotect(encryptedData: buffer,
                                                 optionalEntropy: Entropy,
                                                 scope: DataProtectionScope.CurrentUser);

            return Root.Defaults.Encoding.GetString(result);
        }

        #endregion
    }
}
