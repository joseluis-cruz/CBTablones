using System;
using System.Collections.Generic;

using Android.Widget;
using Android.App;
using Android.Views;

namespace CBTablones
{
	public class ContactoAdapter:BaseAdapter<Contacto>
	{
		List <Contacto> items;
		Activity context;

		public ContactoAdapter (Activity context, List<Contacto> items)
		{
			this.context = context;
			this.items = items;
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var item = items [position];

			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.ListaContactosItemLo, null);
			view.FindViewById<TextView> (Resource.Id.tvAlias).Text = item.Alias.ToString ();

			/*int _IDImagenContacto = context.Resources.GetIdentifier("dorsal_"+item.dorsal.ToString(),"drawable", context.PackageName);
			view.FindViewById<ImageView> (Resource.Id.imgCorredor).SetImageResource (id_img_corredor);*/

			return view;
		}

		public override int Count {
			get {
				return items.Count;
			}
		}

		#endregion

		#region implemented abstract members of BaseAdapter

		public override Contacto this [int index] {
			get {
				return items[index];
			}
		}

		#endregion
	
	}
}

