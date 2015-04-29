
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
{	// ACTIVIDAD PARA LA CREACION DEL ITEM TAB CONTACTOS
	[Activity]			
	public class TabContactos : Activity
	{

		private ListView _LvContactos;
		private List<Contacto> _Contactos;
		private Button _AgregarContacto;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.TabContactos);
			// Create your application here

			Entorno.Init ();

			MostrarContactos ();

			_AgregarContacto = FindViewById<Button> (Resource.Id.addContacto);
			_AgregarContacto.Click += delegate {AbrirVentana (typeof(FormContacto));};

		}

		// MÉTODO PARA OBTENER EL RESULTADO DE LA ACTIVIDAD ABIERTA PARA RESULTADO
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok) {
				MostrarContactos ();
			}
		}

		// MÉTODO GENÉRICO PARA ABRIR VENTANAS
		private void AbrirVentana(Type activityType)
		{
			var myIntent = new Intent (Application.Context, activityType);
			StartActivityForResult (myIntent, 0);
		}

		// METODO PARA MOSTRAR LA LISTA DE CONTACTOS
		private void MostrarContactos(){
			_LvContactos = FindViewById<ListView> (Resource.Id.listViewTabContact);
			var query_contactos = Entorno.DB.Table<Contacto> ();
			this._Contactos = query_contactos.ToList ();
			_LvContactos.Adapter = new ContactoAdapter (this, this._Contactos);
		}
	}
}

