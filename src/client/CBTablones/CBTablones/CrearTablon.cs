
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

namespace CBTablones
{
	[Activity (Label = "Nuevo Tablon")]			
	public class CrearTablon : Activity
	{
		private Button _BtnSiguiente;
		private String[] _ListaCaducidades = { "1 Hora", "1 Dia", "1 Mes", "Nunca" };
		private String[] _ListaVolatilidades = { "5 Segundos", "20 Segundos", "1 Minuto", "1 Hora", "Nunca" };

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.CrearTablon);

			_BtnSiguiente = FindViewById<Button> (Resource.Id.btnCrearTablon);

			//José Luis - OJO: AddContactsTablon no está en el proyecto!!!! - _BtnSiguiente.Click += delegate {AbrirVentana (typeof(AddContactsTablon));};



		}

		private void AbrirVentana (Type activityType)
		{
			var myIntent = new Intent (Application.Context, activityType);
			StartActivity (myIntent);
		}
	}
}

