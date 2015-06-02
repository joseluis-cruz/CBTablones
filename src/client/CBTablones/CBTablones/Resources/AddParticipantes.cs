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
	// ACTIVIDAD PARA LA CREACION DEL ITEM TAB CONTACTOS
	[Activity]			
	public class AddParticipantes : Activity
	{
		private ListView _lvParticipantes;
		private List<Participante> _Participantes;
		private Button _AddParticipante;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AddParticipantes);

			// Create your application here

			Entorno.Init ();
			Entorno.DB.CreateTable<Participante> ();

			MostrarParticipantes ();

			_AddParticipante = FindViewById<Button> (Resource.Id.btAddParticipante);
			// Aquí pretendo abrir la lista de contactos existente, aprovechando la activity
			//de Pedro "ListaContactos" y así poder añadir participantes existentes en la agenda
			_AddParticipante.Click += delegate {AbrirVentana (typeof(ListaContactos));};

		}

		// MÉTODO PARA OBTENER EL RESULTADO DE LA ACTIVIDAD ABIERTA PARA RESULTADO
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok) {
				MostrarParticipantes ();
			}
		}

		// MÉTODO GENÉRICO PARA ABRIR VENTANAS
		private void AbrirVentana(Type activityType)
		{
			var myIntent = new Intent (Application.Context, activityType);
			StartActivityForResult (myIntent, 0);
		}

		// METODO PARA MOSTRAR LA LISTA DE CONTACTOS
		private void MostrarParticipantes(){
			_lvParticipantes = FindViewById<ListView> (Resource.Id.lvParticipantes);
			var query_participantes = Entorno.DB.Table<Participante> ();
			this._Participantes = query_participantes.ToList ();
			_lvParticipantes.Adapter = new ParticipanteAdapter (this, this._Participantes);
		}


		public AddParticipantes ()
		{

		}
	}
}

