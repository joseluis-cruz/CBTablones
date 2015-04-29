
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
	[Activity (Label = "ListaContactos")]			
	public class ListaContactos : Activity
	{

		private ListView _LvContactos;
		private List<Contacto> _Contactos;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ListaContactoLo);

			Entorno.Init ();
			_LvContactos = FindViewById<ListView> (Resource.Id.lvContactos);
			var query_contactos = Entorno.DB.Table<Contacto> ();
			this._Contactos = query_contactos.ToList ();
			_LvContactos.Adapter = new ContactoAdapter (this, this._Contactos);
		}
	


	
	}
}

