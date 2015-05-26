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
{	// ACTIVIDAD PARA LA CREACION DEL ITEM TAB TABLONES
	[Activity]			
	public class TabTablones : Activity
	{

		private ListView _LvTablones;
		private List<Tablon> _LTablon;
		private Button _BtnCrearTablon;
		private Tablon _Tablon = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.TabTablones);

			Entorno.Init ();
			CreaTabla();


			ListarTablones ();

			_BtnCrearTablon = FindViewById<Button> (Resource.Id.btnCrearTablon);

			//Ventana que se abre al pulsar Crear Tablon
			_BtnCrearTablon.Click += delegate {AbrirVentana (typeof(CrearTablon));};

			//Codigo que se ejecuta al pulsar un item de la lista
			_LvTablones.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e)
			{
				//
			};
		}

		// MÉTODO PARA OBTENER EL RESULTADO DE LA ACTIVIDAD ABIERTA PARA RESULTADO
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok) {
				ListarTablones();
			}
		}

		// MÉTODO GENÉRICO PARA ABRIR VENTANAS
		private void AbrirVentana(Type activityType)
		{
			var myIntent = new Intent (Application.Context, activityType);
			StartActivityForResult (myIntent, 0);
		}

		private void ListarTablones(){

			var query_tablones = Entorno.DB.Table<Tablon> ();
			_LvTablones = FindViewById<ListView> (Resource.Id.LvTablones);
			this._LTablon = query_tablones.ToList ();
			_LvTablones.Adapter = new TablonAdapter (this, this._LTablon);
		}

		private void CreaTabla()
		{
			Entorno.DB.CreateTable<Tablon> ();

			_Tablon = new Tablon();
			_Tablon.Id = 1;
			_Tablon.Nombre = "Tablon1";
			_Tablon.Caducidad = DateTime.Parse ("13/01/1986");
			_Tablon.Volatilidad = DateTime.Parse ("14/01/1986");
			_Tablon.Estado = "C";

			Entorno.DB.Insert (_Tablon);
			// al agregar el corredor, es como si pasáramos a modificarlo
			//Toast.MakeText (this, "Dorsal agregado correctamente.", ToastLength.Long).Show ();
		}
	}
}

