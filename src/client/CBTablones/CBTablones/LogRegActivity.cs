using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;

namespace CBTablones
{
	[Activity (Label = "LogRegActivity")]
	public class LogRegActivity : Activity
	{
		public static SQLiteConnection db;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.LogRegLo);

			// Inicializar conexión a la base de datos
			Entorno.Init ();

			// Poned aquí el código que abre vuestra actividad:
			FindViewById<Button> (Resource.Id.btnPrincipalLogin).Click += abreLogin;
			FindViewById<Button> (Resource.Id.btnPrincipalRegistrar).Click += abreRegistrar;
		}
		public void abreLogin(object sender, EventArgs e)
		{
			Intent intent;
			intent = new Intent (this, typeof(LoginActivity));
			StartActivity (intent);
		}
		public void abreRegistrar(object sender, EventArgs e)
		{
			Intent intentRegistrar;
			intentRegistrar = new Intent (this, typeof(RegistrarActivity));
			StartActivity (intentRegistrar);
		}
	}
}


