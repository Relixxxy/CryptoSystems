using System.Text;

namespace CryptoSystems.Services;

public class LabOneCryptoService
{
    private IList<byte> _gammaKey = null!;

    public LabOneCryptoService()
    {
        var polynomial = new int[] { 46, 8, 7, 6, 0 };
        SetGammaKeyWithLFSR(polynomial);
    }

    public string Encrypt(string plaintext)
    {
        byte[] encryptedBytes = new byte[plaintext.Length];

        for (int i = 0; i < plaintext.Length; i++)
        {
            encryptedBytes[i] = (byte)(plaintext[i] ^ _gammaKey[i % _gammaKey.Count]);
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
            decryptedBytes[i] = (byte)(encryptedBytes[i] ^ _gammaKey[i % _gammaKey.Count]);
        }

        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        return decryptedText;
    }

    public void SetGammaKeyWithLFSR(int[] polynomial)
    {
        if (polynomial is null
            || polynomial.Length < 1
            || !polynomial
                .OrderDescending()
                .SequenceEqual(polynomial))
        {
            throw new ArgumentException("Bad polynomial");
        }

        var bytes = InitBytesWithPolynomial(polynomial);
        _gammaKey = GetGammaKeyWithLFSR(polynomial, bytes);
    }

    private IList<byte> GetGammaKeyWithLFSR(int[] polynomial, byte[] bytes)
    {
        var gammaKey = new List<byte>(bytes.Length);
        var shiftRegister = new Queue<byte>(bytes);

        for (int i = 0; i < bytes.Length; i++)
        {
            var feedbackBytes = shiftRegister.Where((b, j) => polynomial.Contains(j)).ToArray();
            byte newByte = XoR(feedbackBytes);

            gammaKey.Add(newByte);

            shiftRegister.Enqueue(newByte);
            shiftRegister.Dequeue();

            Console.WriteLine("Gamma Key");
            Console.WriteLine(string.Join(", ", gammaKey));
            Console.WriteLine("Shift Register");
            Console.WriteLine(string.Join(", ", shiftRegister));
        }

        return gammaKey.ToArray();
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
