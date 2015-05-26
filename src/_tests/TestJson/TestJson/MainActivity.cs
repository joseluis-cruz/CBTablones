using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;

namespace TestJson
{
	[Activity (Label = "TestJson", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {

				// PARTE 1: De json a objeto (Deserialización)
				// Parsear el objeto json en texto y construir un objeto "obj" con sus datos
				var obj = JsonObject.Parse ("{\n    \"nombre\": \"Jose Luis\",\n    \"apellido\": \"Cruz\",\n    \"direccion\": {\n        \"tipo_via\": \"c/\",\n        \"nombre_via\": \"Mayor\",\n        \"numero\": \"25\",\n        \"codigo_postal\": \"30123\",\n        \"localidad\": \"Cartagena\"\n    },\n    \"emails\": [\n        \"primera@email.com\",\n        \"segunda@email.com\"\n    ]\n}");
				Console.WriteLine ("Nombre = {0}", obj ["nombre"]);
				Console.WriteLine ("Vía = {0}", obj ["direccion"] ["nombre_via"]);
				Console.WriteLine ("2º email = {0}", obj ["emails"] [1]);

				// PARTE 2: De objeto a json (Serialización)
				// Crear un objeto de la clase "Persona"
				var p1 = new Persona ();
				p1.Nombre = "José Luis";
				p1.Apellidos = "Cruz";
				p1.Direccion.Tipo_Via = "C/";
				p1.Direccion.Nombre_Via = "Mayor";
				p1.Direccion.Numero = 12;
				p1.Direccion.Codigo_Postal = "30321";
				p1.Direccion.Localidad = "Cartagena";
				p1.Emails = new string[2];
				p1.Emails [0] = "primero@email.com";
				p1.Emails [1] = "segundo@email.com";

				Persona[] personas = new Persona[3];
				personas [0] = new Persona ();
				personas [1] = new Persona ();
				personas [2] = new Persona ();
				personas [0].Nombre = "Paco";
				personas [1].Nombre = "Pepe";
				personas [2].Nombre = "Juan";

				var listapersonas = new List<Persona> ();
				p1 = new Persona ();
				p1.Nombre = "Primero";
				p1.Apellidos = "De La Lista";
				listapersonas.Add (p1);
				p1 = new Persona ();
				p1.Nombre = "Segundo";
				p1.Apellidos = "De La Lista";
				listapersonas.Add (p1);
				p1 = new Persona ();
				p1.Nombre = "Tercero";
				p1.Apellidos = "De La Lista";
				listapersonas.Add (p1);


				// Serializar el objeto a json
				MemoryStream stream1 = new MemoryStream ();
				DataContractJsonSerializer ser = new DataContractJsonSerializer (typeof(Persona));
				// ser.WriteObject (stream1, p1);
				// ser.WriteObject (stream1, personas);
				ser.WriteObject (stream1, listapersonas.ToArray ());
				stream1.Position = 0;
				StreamReader sr = new StreamReader (stream1);
				string objetoentexto = sr.ReadToEnd ();
				Console.WriteLine ("Objeto json = {0}", objetoentexto);

			};
		}
	}
}


