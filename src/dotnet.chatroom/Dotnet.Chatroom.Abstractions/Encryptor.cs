using System.Text;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Encryptor : IEncryptor
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public virtual string Decrypt(string value)
		{
			try
			{
				byte[] passwordBytes = Convert.FromBase64String(value);

				return Encoding.UTF8.GetString(passwordBytes);
			}
			catch (Exception)
			{
				return value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public virtual string Encrypt(string value)
		{
			try
			{
				byte[] passwordBytes = Encoding.UTF8.GetBytes(value);

				return Convert.ToBase64String(passwordBytes);
			}
			catch (Exception)
			{
				return value;
			}
		}
	}
}
