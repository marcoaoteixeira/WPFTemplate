using System.Security.Cryptography;

namespace Nameless.WPF {
    public static class CryptoHelper {
        #region Private Static Read-Only Fields

        private static readonly byte[] Entropy = "8a33981b-a36d-4c28-8f52-bd1ff82f44ae"u8.ToArray();

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
