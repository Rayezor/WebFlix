using WebFlix.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebFlixClient
{
	public class Program
	{
		static void Main(string[] args)
		{
			Task result = SendMessageAsync();
			result.Wait();
		}

		private static async Task SendMessageAsync()
		{
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://localhost:5006/");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				TextMessage txt = new TextMessage() { FromNumber = "086-555444", ToNumber = "086-444555", Content = "Message from the client" };

				HttpResponseMessage response = await client.PostAsJsonAsync("api/SMS", txt);

				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Message sent");
				}
				else
				{
					Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}