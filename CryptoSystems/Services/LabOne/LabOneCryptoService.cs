using System.Text;
using System.Text.RegularExpressions;

namespace CryptoSystems.Services.LabOne;

public class LabOneCryptoService
{
    private int[] _polynomial = null!;

    public IList<byte> GammaKey { get; private set; } = null!;

    public string Encrypt(string plaintext)
    {
        if (GammaKey is null)
        {
            throw new Exception("Gamma Key can't be null");
        }

        var encryptedBytes = new byte[plaintext.Length];

        for (int i = 0; i < plaintext.Length; i++)
        {
            encryptedBytes[i] = (byte)(plaintext[i] ^ GammaKey[i % GammaKey.Count]);
        }

        var encryptedText = Convert.ToBase64String(encryptedBytes);

        return encryptedText;
    }

    public string Decrypt(string encryptedText)
    {
        if (GammaKey is null)
        {
            throw new Exception("Gamma Key can't be null");
        }

        var encryptedBytes = Convert.FromBase64String(encryptedText);
        var decryptedBytes = new byte[encryptedBytes.Length];

        for (int i = 0; i < encryptedBytes.Length; i++)
        {
            decryptedBytes[i] = (byte)(encryptedBytes[i] ^ GammaKey[i % GammaKey.Count]);
        }

        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        return decryptedText;
    }

    public void SetPolynomial(string input)
    {
        string pattern = @"^\d+(?:\s\d+)*$";

        if (!Regex.IsMatch(input, pattern))
        {
            throw new ArgumentException("Invalid format");
        }

        var polynomial = input.Split(" ").Select(n => int.Parse(n)).ToArray();

        if (polynomial is null
            || polynomial.Length < 1
            || polynomial.Distinct().Count() != polynomial.Length
            || !polynomial
                .OrderDescending()
                .SequenceEqual(polynomial))
        {
            throw new ArgumentException("Bad polynomial");
        }

        _polynomial = polynomial;
    }

    public void GenerateGammaKeyWithLFSR()
    {
        if (_polynomial is null)
        {
            throw new Exception("Polynomial can't be null");
        }

        var bytes = InitBytesWithPolynomial(_polynomial);
        var gammaKey = new List<byte>(bytes.Length);
        var shiftRegister = new Queue<byte>(bytes);

        for (int i = 0; i < bytes.Length; i++)
        {
            var feedbackBytes = shiftRegister.Where((b, j) => _polynomial.Contains(j)).ToArray();
            byte newByte = XoR(feedbackBytes);

            gammaKey.Add(newByte);

            shiftRegister.Enqueue(newByte);
            shiftRegister.Dequeue();
        }

        GammaKey = gammaKey.ToArray();
    }

    private byte XoR(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentException("Input byte array is empty or null");
        }

        byte result = bytes[0];

        for (int i = 1; i < bytes.Length; i++)
        {
            result ^= bytes[i];
        }

        return result;
    }

    private byte[] InitBytesWithPolynomial(int[] polynomial)
    {
        int keyLength = polynomial.First() + 1;
        var bytes = new byte[keyLength];

        for (int i = 0; i < polynomial.Length; i++)
        {
            bytes[polynomial[i]] = 1;
        }

        return bytes;
    }
}
