using System.Security.Cryptography;
using System.Text;

public static class CryptoUtil
{
    public static string GenerateHash(string inputString)
    {
        var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha256.ComputeHash(bytes);
        return GetHexadecimalString(hash);
    }

    public static string GetHexadecimalString(byte[] bytes)
    {
        var result = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            result.Append(bytes[i].ToString("X2"));
        }
        return result.ToString();
    }
}