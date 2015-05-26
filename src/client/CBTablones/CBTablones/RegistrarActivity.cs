
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

namespace CBTablones
{
	[Activity (Label = "RegistrarActivity")]			
	public class RegistrarActivity : Activity
	{
		private DatosUsuario usuario;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.RegistrarLo);

			Entorno.Init ();
			Entorno.DB.CreateTable<DatosUsuario> ();
			//Entorno.DB.Execute ("DROP TABLE IF EXISTS DATOS_USUARIO");

			FindViewById<Button> (Resource.Id.btnRegistrarEnviar).Click += delegate {
				usuario = new DatosUsuario();
				usuario.ID = 1;
				usuario.Nombre = FindViewById<EditText>(Resource.Id.etRegistrarNombre).Text;
				usuario.Alias = FindViewById<EditText>(Resource.Id.etRegistrarUsuario).Text;
				usuario.Contrasenya = FindViewById<EditText>(Resource.Id.etRegistrarPassword).Text;
				usuario.Caducidad = Convert.ToDateTime(FindViewById<EditText>(Resource.Id.etRegistrarCaducidad).Text);
				usuario.Volatilidad = Convert.ToDateTime(FindViewById<EditText>(Resource.Id.etRegistrarVolatilidad).Text);
				usuario.Estado = FindViewById<EditText>(Resource.Id.etRegistrarEstado).Text;
				usuario.EMail = FindViewById<EditText>(Resource.Id.etRegistrarEmail).Text;
				usuario.Servidor = FindViewById<EditText>(Resource.Id.etRegistrarServidor).Text;
				Entorno.DB.Insert(usuario);
				Toast.MakeText(this, "Se ha creado un nuevo usuario", ToastLength.Long).Show();
			};
		}
	}
}

