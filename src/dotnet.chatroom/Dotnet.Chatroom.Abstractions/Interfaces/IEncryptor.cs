namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public interface IEncryptor
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string Encrypt(string value);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string Decrypt(string value);
	}
}
