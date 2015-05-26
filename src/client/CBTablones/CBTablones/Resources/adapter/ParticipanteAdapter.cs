
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
	[Activity (Label = "ParticipanteAdapter")]			
	public class ParticipanteAdapter : Activity
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

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = items [position];

			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.AddParticipantes, null);
			view.FindViewById<TextView> (Resource.Id.tvParticipante).Text = item.NombreContacto;

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

