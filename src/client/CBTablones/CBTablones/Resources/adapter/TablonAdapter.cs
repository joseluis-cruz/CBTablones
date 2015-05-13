using System;
using System.Collections.Generic;

using Android.Widget;
using Android.App;
using Android.Views;

namespace CBTablones
{
	public class TablonAdapter:BaseAdapter<Tablon>
	{
		List <Tablon> items;
		Activity context;

		public TablonAdapter (Activity context, List<Tablon> items)
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
				view = context.LayoutInflater.Inflate (Resource.Layout.ListaTablonesItemLo, null);
			view.FindViewById<TextView> (Resource.Id.tvNombre).Text = item.Nombre.ToString ();

			return view;
		}

		public override int Count {
			get {
				return items.Count;
			}
		}

		#endregion

		#region implemented abstract members of BaseAdapter

		public override Tablon this [int index] {
			get {
				return items[index];
			}
		}

		#endregion
	}
}

