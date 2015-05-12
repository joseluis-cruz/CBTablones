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
	// ACTIVIDAD PARA AÑADIR CONTACTOS A UN TABLÓN
	[Activity]
	public class AddContacts : Activity
	{
		private ListView _LvContactos;
		private List<Contacto> _Contactos;
		private Button _AgregarContacto;

		// METODO PARA MOSTRAR LA LISTA DE CONTACTOS
		private void MostrarContactos(){
			_LvContactos = FindViewById<ListView> (Resource.Id.listViewTabContact);
			var query_contactos = Entorno.DB.Table<Contacto> ();
			this._Contactos = query_contactos.ToList ();
			_LvContactos.Adapter = new ContactoAdapter (this, this._Contactos);
		}

		public AddContacts ()
		{
			
		}
	}
}

