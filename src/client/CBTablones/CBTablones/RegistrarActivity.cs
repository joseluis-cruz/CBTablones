
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using System.Json;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CBTablones
{
	[Activity (Label = "RegistrarActivity")]			
	public class RegistrarActivity : Activity
	{
		private Spinner _spCaducidad;
		private Spinner _spVolatilidad;
		private String[] _ListaCaducidades =   {"1 Hora","1 Dia","1 Mes","Nunca"};
		private String[] _ListaVolatilidades = {"5 Segundos","20 Segundos","1 Minuto","1 Hora","Nunca"};
		private DatosUsuario _usuario;
		private DateTime _Caducidad;
		private DateTime _Volatilidad;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.RegistrarLo);

			Entorno.Init ();
			Entorno.DB.Execute ("DROP TABLE IF EXISTS DATOS_USUARIO");
			Entorno.DB.CreateTable<DatosUsuario> ();

			_spCaducidad = FindViewById<Spinner> (Resource.Id.spRegistrarCaducidad);
			_spCaducidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerCad_ItemSelected);
			_spCaducidad.Adapter = new ArrayAdapter(this,Resource.Layout.TextViewItemLo,this._ListaCaducidades);

			_spVolatilidad = FindViewById<Spinner> (Resource.Id.spRegistrarVolatilidad);
			_spVolatilidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerVol_ItemSelected);
			_spVolatilidad.Adapter = new ArrayAdapter (this,Resource.Layout.TextViewItemLo,this._ListaVolatilidades);

			FindViewById<Button> (Resource.Id.btnRegistrarEnviar).Click += delegate {
				_usuario = new DatosUsuario();
				_usuario.ID = 1;
				_usuario.Nombre = FindViewById<EditText>(Resource.Id.etRegistrarNombre).Text;
				_usuario.Alias = FindViewById<EditText>(Resource.Id.etRegistrarUsuario).Text;
				_usuario.Contrasenya = FindViewById<EditText>(Resource.Id.etRegistrarPassword).Text;
				_usuario.Caducidad = this._Caducidad;
				_usuario.Volatilidad = this._Volatilidad;
				_usuario.Estado = FindViewById<EditText>(Resource.Id.etRegistrarEstado).Text;
				_usuario.EMail = FindViewById<EditText>(Resource.Id.etRegistrarEmail).Text;
				_usuario.Servidor = FindViewById<EditText>(Resource.Id.etRegistrarServidor).Text;
				Entorno.DB.Insert(_usuario);
				Toast.MakeText(this, "Se ha creado un nuevo usuario", ToastLength.Long).Show();
				//IP DE JULIO : 10.32.73.78
				MemoryStream stream1 = new MemoryStream ();
				DataContractJsonSerializer ser = new DataContractJsonSerializer (typeof(DatosUsuario));
				// ser.WriteObject (stream1, p1);
				// ser.WriteObject (stream1, personas);
				ser.WriteObject (stream1, _usuario);
				stream1.Position = 0;
				StreamReader sr = new StreamReader (stream1);
				string objetoentexto = sr.ReadToEnd ();
				Console.WriteLine ("Objeto json = {0}", objetoentexto);

				Cliente.Url = "http://10.32.73.78/api";
				Cliente.sendCmd ("", "", "UserRegistration", objetoentexto);
				
			};
		}

		private void spinnerCad_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			switch (e.Position) 
			{
			default:
			case 0:
				_Caducidad = new DateTime ().AddHours (1);
				break;
			case 1:
				_Caducidad = new DateTime ().AddDays (1);
				break;
			case 2:
				_Caducidad = new DateTime ().AddMonths (1);
				break;
			case 3:
				_Caducidad = new DateTime ().AddMonths (99);
				break;
			}
		}

		private void spinnerVol_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			switch (e.Position) 
			{
			default:
			case 0:
				_Volatilidad = new DateTime ().AddSeconds (5);
				break;
			case 1:
				_Volatilidad = new DateTime ().AddSeconds (20);
				break;
			case 2:
				_Volatilidad = new DateTime ().AddMinutes (1);
				break;
			case 3:
				_Volatilidad = new DateTime ().AddHours (1);
				break;
			case 4:
				_Volatilidad = new DateTime ().AddHours (99);
				break;
			}
		}
	}
}

