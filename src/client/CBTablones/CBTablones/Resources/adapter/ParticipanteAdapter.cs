﻿using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace CBTablones
{
	public class ParticipanteAdapter : BaseAdapter<Participante>
	{
		List <Participante> items;
		Activity context;

		public ParticipanteAdapter (Activity context, List<Participante> items)
		{
			this.context = context;
			this.items = items;
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, View convertView, Android.Views.ViewGroup parent)
		{
			var item = items [position];

			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.AddParticipantesItemLo, null);
			var _ImageView = view.FindViewById <ImageView> (Resource.Id.imgParticipante);
			view.FindViewById <TextView> (Resource.Id.tvParticipante).Text = item.NombreP;
			view.FindViewById <TextView> (Resource.Id.tvAlias).Text = item.AliasP;

			// Código de Pedro; Entiend que para cargar la imágen
			App._dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CTablones");
			App._file = new Java.IO.File(App._dir, String.Format("photo_{0}.jpg", item.IDP));
			int height = 100;
			int width = 100 ;
			App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);

			if (App.bitmap != null) {
				view.FindViewById<ImageView>(Resource.Id.imgParticipante).SetImageBitmap (App.bitmap);
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

		public override Participante this [int index] {
			get {
				return items[index];
			}
		}

		#endregion

	}
}

