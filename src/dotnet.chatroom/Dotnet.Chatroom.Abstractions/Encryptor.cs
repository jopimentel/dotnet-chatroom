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
		public string Decryt(string value)
		{
			byte[] passwordBytes = Convert.FromBase64String(value); 

			return Encoding.UTF8.GetString(passwordBytes);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public string Encryt(string value)
		{
			byte[] passwordBytes = Encoding.UTF8.GetBytes(value);

			return Convert.ToBase64String(passwordBytes);
		}
	}
}
