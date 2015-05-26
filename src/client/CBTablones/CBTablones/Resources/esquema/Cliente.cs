using System;
using System.IO;
using System.Net;
using System.Text;
using System.Json;

namespace CBTablones
{
	public static class Cliente
	{
		public static string Url { get; set; }

		private static void processResponse (string responseString)
		{
			// todo por hacer
			var jsonres = JsonObject.Parse (responseString);

			Console.WriteLine ("Nombre 1 = {0}", jsonres [0] ["nombre"]);
			Console.WriteLine ("Nombre 2 = {0}", jsonres [1] ["nombre"]);
		}

		public static int sendCmd (string usuario, string password, string comando, string jsonarg)
		{

			var webRequest = WebRequest.Create (Url + "/" + comando); // "http://10.32.73.139:51995/api/Mensajes"
			webRequest.Timeout = 60000;
			webRequest.Method = WebRequestMethods.Http.Post;
			var authInfo = string.Format ("{0}:{1}", usuario, password);
			var authInfoEncoded = Convert.ToBase64String (Encoding.Default.GetBytes (authInfo));
			webRequest.Headers ["Authorization"] = string.Format ("Basic {0}", authInfoEncoded);	

			var postData = jsonarg; // "{\"employees\":[{\"firstName\":\"John\",\"lastName\":\"Doe\"},{\"firstName\":\"Anna\",\"lastName\":\"Smith\"},{\"firstName\":\"Peter\",\"lastName\":\"Jones \"}]}";
			var data = Encoding.ASCII.GetBytes (postData);

			webRequest.ContentType = "application/json";	 // application/x-www-form-urlencoded
			webRequest.ContentLength = data.Length;
			using (var stream = webRequest.GetRequestStream ()) {
				stream.Write (data, 0, data.Length);
			}

			try {
				var result = (HttpWebResponse)webRequest.GetResponse ();
				var responseStream = result.GetResponseStream ();
				var responseString = new StreamReader (responseStream).ReadToEnd ();
				processResponse (responseString);
			} catch (Exception ex) {
				// error de conexión al servidor
				Console.WriteLine ("Error al recuperar respuesta HTTP: " + ex.Message);
			}

			return 0;
		}

	}
}

