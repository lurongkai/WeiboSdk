using System;
using System.IO;
using System.Security.Cryptography;


namespace NetDimension.Strings.Crypto
{
	public static class DESC
	{

		//随机选8个字节既为密钥也为初始向量 
		private static byte[] KEY_64 = { 33, 16, 93, 156, 78, 4, 218, 32 };
		private static byte[] IV_64 = { 99, 103, 246, 79, 36, 99, 167, 3 };

		//对TripleDES,采取24字节或192位的密钥和初始向量 
		private static byte[] KEY_192 = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 194 };
		private static byte[] IV_192 = { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 181 };

		/// <summary>
		/// 标准的DES加密
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Encrypt(string value)
		{
			if (value == null || value == "")
				return string.Empty;

			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
			StreamWriter sw = new StreamWriter(cs);

			sw.Write(value);
			sw.Flush();
			cs.FlushFinalBlock();
			ms.Flush();

			//再转换为一个字符串 
			return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
		}

		//标准的DES解密 
		/// <summary>
		/// 标准的DES解密
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Decrypt(string value)
		{
			if (value == null || value == "")
				return string.Empty;

			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

			//从字符串转换为字节组 
			byte[] buffer = Convert.FromBase64String(value);
			MemoryStream ms = new MemoryStream(buffer);
			CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cs);

			return sr.ReadToEnd();
		}

		//TRIPLE DES加密 
		public static string EncryptTripleDES(string value)
		{
			if (value == null || value == "")
				return string.Empty;

			TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
			StreamWriter sw = new StreamWriter(cs);

			sw.Write(value);
			sw.Flush();
			cs.FlushFinalBlock();
			ms.Flush();

			//再转换为一个字符串 
			return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
		}

		//TRIPLE DES解密 
		public static string DecryptTripleDES(string value)
		{
			if (value == null || value == "")
				return string.Empty;

			TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();

			//从字符串转换为字节组 
			byte[] buffer = Convert.FromBase64String(value);
			MemoryStream ms = new MemoryStream(buffer);
			CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cs);

			return sr.ReadToEnd();
		}

	} 

}
