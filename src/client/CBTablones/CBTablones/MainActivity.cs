using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace CBTablones
{
	[Activity (Label = "Tablones", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : TabActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Inicializar conexión a la base de datos
			Entorno.Init ();

			// SE AÑADEN LOS ITEMS DEL TABHOST
			CreateTab(typeof(TabContactos), "tab_contactos", "Contactos");
			CreateTab(typeof(TabConversaciones), "tab_conversaciones", "Mensajes");
			CreateTab(typeof(TabTablones), "tab_tablones", "Tablones");



		}

		// MÉTODO PARA CREAR LOS ITEMS DEL TABHOST
		private void CreateTab(Type activityType, string tag, string label)
		{
			var intent = new Intent(this, activityType);
			intent.AddFlags(ActivityFlags.NewTask);

			var spec = TabHost.NewTabSpec(tag);
			spec.SetIndicator(label);
			spec.SetContent(intent);

			TabHost.AddTab(spec);
		}
	}
}


