using System.Text;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Default implementation of the <see cref="IEncryptor"/> interface.
	/// </summary>
	public class Encryptor : IEncryptor
	{
		/// <summary>
		/// Decrypt a ciphertext.
		/// </summary>
		/// <param name="value">Refers to the ciphertext to be decrypted.</param>
		/// <returns>Decrypted <see cref="string"/> value representation of the ciphertext.</returns>
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
		/// Gets a ciphertext from the specified <see cref="string"/> value.
		/// </summary>
		/// <param name="value"><see cref="string"/> value to be encrypted.</param>
		/// <returns>The ciphertext that is the representation of the specified value.</returns>
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
