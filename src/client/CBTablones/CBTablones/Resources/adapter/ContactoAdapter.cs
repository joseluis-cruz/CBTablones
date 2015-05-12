using System;
using System.Collections.Generic;

using Android.Widget;
using Android.App;
using Android.Views;
using Android.Graphics;
using Android.Content.PM;

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
			var _ImageView =view.FindViewById<ImageView>(Resource.Id.imgContacto);

			App._dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CTablones");
			App._file = new Java.IO.File(App._dir, String.Format("photo_{0}.jpg", item.ID));
			int height = 100;
			int width = 100 ;
			App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);

			if (App.bitmap != null) {
				view.FindViewById<ImageView>(Resource.Id.imgContacto).SetImageBitmap (App.bitmap);
				App.bitmap = null;
			}



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

