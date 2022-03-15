namespace Dotnet.Chatroom
{
	/// <summary>
	/// Defines the encryptation service.
	/// </summary>
	public interface IEncryptor
	{
		/// <summary>
		/// Decrypt a ciphertext.
		/// </summary>
		/// <param name="value">Refers to the ciphertext to be decrypted.</param>
		/// <returns>Decrypted <see cref="string"/> value representation of the ciphertext.</returns>
		string Decrypt(string value);
		/// <summary>
		/// Gets a ciphertext from the specified <see cref="string"/> value.
		/// </summary>
		/// <param name="value"><see cref="string"/> value to be encrypted.</param>
		/// <returns>The ciphertext that is the representation of the specified value.</returns>
		string Encrypt(string value);
	}
}
