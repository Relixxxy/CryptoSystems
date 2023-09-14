using System.Security.Cryptography;
using System.Text;

namespace CryptoSystems.Services;

public class LabOneCryptoService
{
    private byte[] _gammaKey;

    public LabOneCryptoService()
    {
        int keyLength = 32;
        _gammaKey = new byte[keyLength];

        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(_gammaKey);
    }

    public void SetGammaKeyWithLFSR(int[] polynomial)
    {
        if (polynomial == null && polynomial.Length < 1)
        {
            throw new ArgumentException("Bad polynomial");
        }

        int keyLength = polynomial.First();
    }

    public string Encrypt(string plaintext)
    {
        byte[] encryptedBytes = new byte[plaintext.Length];

        for (int i = 0; i < plaintext.Length; i++)
        {
            encryptedBytes[i] = (byte)(plaintext[i] ^ _gammaKey[i % _gammaKey.Length]);
        }

        var encryptedText = Convert.ToBase64String(encryptedBytes);

        return encryptedText;
    }

    public string Decrypt(string encryptedText)
    {
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        var decryptedBytes = new byte[encryptedBytes.Length];

        for (int i = 0; i < encryptedBytes.Length; i++)
        {
            decryptedBytes[i] = (byte)(encryptedBytes[i] ^ _gammaKey[i]);
        }

        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        return decryptedText;
    }
}
