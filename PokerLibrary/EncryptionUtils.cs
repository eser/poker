using System;
using System.Security.Cryptography;
using System.Text;

namespace PokerLibrary {
	public static class EncryptionUtils {
		public static RSACryptoServiceProvider CreateRSAForDecoder(out byte[] publicKey) {
			const int PROVIDER_RSA_FULL = 1;

			CspParameters _cspParams = new CspParameters(PROVIDER_RSA_FULL) {
				KeyContainerName = "Poker2Container",
				Flags = CspProviderFlags.UseUserProtectedKey
			};

			RSACryptoServiceProvider _rsaProvider = new RSACryptoServiceProvider(1024, _cspParams) {
				PersistKeyInCsp = true
			};

			publicKey = _rsaProvider.ExportCspBlob(false);

			return _rsaProvider;
		}

		public static RSACryptoServiceProvider LoadRSAForEncoder(byte[] publicKey) {
			const int PROVIDER_RSA_FULL = 1;

			CspParameters _cspParams = new CspParameters(PROVIDER_RSA_FULL) {
				KeyContainerName = "Poker2Container",
				Flags = CspProviderFlags.UseUserProtectedKey
			};

			RSACryptoServiceProvider _rsaProvider = new RSACryptoServiceProvider(1024, _cspParams) {
				PersistKeyInCsp = true
			};

			_rsaProvider.ImportCspBlob(publicKey);

			return _rsaProvider;
		}

		public static byte[] RSAEncrypt(RSACryptoServiceProvider provider, string input) {
			byte[] _str = Encoding.Default.GetBytes(input);

			return EncryptionUtils.RSAEncrypt(provider, _str);
		}

		public static byte[] RSAEncrypt(RSACryptoServiceProvider provider, byte[] input) {
			return provider.Encrypt(input, true);
		}

		public static byte[] RSADecrypt(RSACryptoServiceProvider provider, byte[] input) {
			byte[] _bytes = provider.Decrypt(input, true);

			return _bytes;
		}

		public static byte[] SymmetricEncrypt<T>(string key, string input) where T : SymmetricAlgorithm, new() {
			byte[] _str = Encoding.Default.GetBytes(input);

			return EncryptionUtils.SymmetricEncrypt<T>(key, _str);
		}

		public static byte[] SymmetricEncrypt<T>(string key, byte[] input) where T : SymmetricAlgorithm, new() {
			DeriveBytes _rgb = new Rfc2898DeriveBytes(key, Encoding.Default.GetBytes("esersalt"));
			byte[] _result;

			using(SymmetricAlgorithm _provider = new T()) {
				_provider.Key = _rgb.GetBytes(_provider.KeySize >> 3);
				_provider.IV = _rgb.GetBytes(_provider.BlockSize >> 3);

				ICryptoTransform _transformer = _provider.CreateEncryptor();
				_result = _transformer.TransformFinalBlock(input, 0, input.Length);
			}

			return _result;
		}

		public static byte[] SymmetricDecrypt<T>(string key, byte[] input) where T : SymmetricAlgorithm, new() {
			DeriveBytes _rgb = new Rfc2898DeriveBytes(key, Encoding.Default.GetBytes("esersalt"));
			byte[] _result;

			using(SymmetricAlgorithm _provider = new T()) {
				_provider.Key = _rgb.GetBytes(_provider.KeySize >> 3);
				_provider.IV = _rgb.GetBytes(_provider.BlockSize >> 3);

				ICryptoTransform _transformer = _provider.CreateDecryptor();
				_result = _transformer.TransformFinalBlock(input, 0, input.Length);
			}

			return _result;
		}

		public static byte[] Hash<T>(string input) where T : HashAlgorithm, new() {
			byte[] _input = Encoding.Default.GetBytes(input);
			byte[] _salt = Encoding.Default.GetBytes("esersalt");

			byte[] _str = new byte[_input.Length + _salt.Length];

			Array.Copy(_input, 0, _str, 0, _input.Length);
			Array.Copy(_salt, 0, _str, _input.Length, _salt.Length);

			return EncryptionUtils.Hash<T>(_str);
		}

		public static byte[] Hash<T>(byte[] input) where T : HashAlgorithm, new() {
			byte[] _result;

			using(HashAlgorithm _provider = new T()) {
				_result = _provider.ComputeHash(input);
			}

			return _result;
		}
	}
}
