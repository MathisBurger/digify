using System.Security.Cryptography;

namespace digify.Util;

/// <summary>
/// A collections of utils creating random values
/// </summary>
public class CryptoRandom
{
    /// <summary>
    /// Creates an array of random bytes
    /// </summary>
    /// <param name="size">The number of random bytes</param>
    /// <returns>The array of random bytes</returns>
    public static byte[] GetBytes(uint size)
    {
        var bytes = new byte[size];
        new RNGCryptoServiceProvider().GetBytes(bytes);
        return bytes;
    }
}