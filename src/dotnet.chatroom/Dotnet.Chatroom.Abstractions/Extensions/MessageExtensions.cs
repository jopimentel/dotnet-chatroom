namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public static class MessageExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="encryptor"></param>
		/// <returns></returns>
		public static T Encrypt<T>(this Message<T> message, IEncryptor encryptor)
		{
			if (message.Content is not string)
				return message.Content;

			string content = encryptor.Encrypt(message.Content.ToString());
			object box = content;

			return (T)box;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="encryptor"></param>
		/// <returns></returns>
		public static T Decrypt<T>(this Message<T> message, IEncryptor encryptor)
		{
			if (message.Content is not string)
				return message.Content;

			string content = encryptor.Decrypt(message.Content.ToString());
			object box = content;

			return (T)box;
		}
	}
}
