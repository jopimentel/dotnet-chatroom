using System.Net.Mime;

namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// Extends the <see cref="HttpResponseMessage"/> types by adding additional functionalities.
	/// </summary>
	internal static class HttpResponseMessageExtensions
	{
		/// <summary>
		/// Gets the name of the file which is part of the content of the <see cref="HttpResponseMessage"/>.
		/// </summary>
		/// <param name="response">The <see cref="HttpResponseMessage"/> object where the file is located.</param>
		/// <returns>The name of the file. <see langword="null"/> will be returned if no file is found.</returns>
		public static string GetFilename(this HttpResponseMessage response)
		{
			bool exists = response.Content.Headers.TryGetValues("Content-Disposition", out IEnumerable<string> values);

			if (!exists)
				return null;

			string disposition = values.FirstOrDefault();
			ContentDisposition contentDisposition = new(disposition);

			if (contentDisposition.DispositionType != "attachment")
				return null;

			return contentDisposition.FileName;
		}
	}
}
