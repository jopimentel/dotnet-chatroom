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
		string Encryt(string value);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string Decryt(string value);
	}
}
