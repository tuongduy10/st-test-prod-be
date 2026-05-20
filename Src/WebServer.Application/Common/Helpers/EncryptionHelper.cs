namespace WebServer.Application.Common.Helpers;

public static class EncryptionHelper
{
    public static string Encrypt(string plainText, string encryptKey)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptKey.PadRight(32).Substring(0, 32));
        aes.IV = new byte[16]; // Default IV (all zeros)

        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (StreamWriter writer = new(cryptoStream))
        {
            writer.Write(plainText);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string cipherText, string encryptKey)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptKey.PadRight(32).Substring(0, 32));
        aes.IV = new byte[16]; // Default IV (all zeros)

        using MemoryStream memoryStream = new(Convert.FromBase64String(cipherText));
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader reader = new(cryptoStream);

        return reader.ReadToEnd();
    }
}
