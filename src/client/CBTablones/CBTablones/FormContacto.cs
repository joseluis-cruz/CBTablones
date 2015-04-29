
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
	[Activity (Label = "FormContacto")]			
	public class FormContacto : Activity
	{
		private Button _BtnAceptar;
		private Button _BtnCancelar;
		private Button _BtnMostrarLista;
		private Spinner _SpCaducidad;
		private Spinner _SpVolatilidad;
		private String[] _ListaCaducidades = {"1 Hora","1 Dia","1 Mes","Nunca"};
		private String[] _ListaVolatilidades={"5 Segundos","20 Segundos","1 Minuto","1 Hora","Nunca"};
		private DateTime _Caducidad;
		private DateTime _Volatilidad;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.FormContactoLo);

			// Inicializar conexión a la base de datos
			Entorno.Init ();
			Entorno.DB.CreateTable<Contacto> ();

			_BtnAceptar = FindViewById<Button> (Resource.Id.btnAceptar);
			_BtnCancelar = FindViewById<Button> (Resource.Id.btnCancelar);
			_BtnMostrarLista = FindViewById<Button> (Resource.Id.btnMostrarLista);

			_BtnAceptar.Click += delegate {
				Agregar ();
			};
			_BtnCancelar.Click += delegate {
				Cancelar ();
			};

			_BtnMostrarLista.Click += delegate {
				Mostrar ();
			};
				
			_SpCaducidad = FindViewById<Spinner> (Resource.Id.spCaducidad);
			_SpCaducidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerCad_ItemSelected);
			_SpCaducidad.Adapter = new ArrayAdapter(this,Resource.Layout.TextViewItemLo,this._ListaCaducidades);

			_SpVolatilidad = FindViewById<Spinner> (Resource.Id.spVolatilidad);
			_SpVolatilidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerVol_ItemSelected);
			_SpVolatilidad.Adapter = new ArrayAdapter (this,Resource.Layout.TextViewItemLo,this._ListaVolatilidades);
		}

		public void Agregar()
		{
			String _Nombre=FindViewById<EditText> (Resource.Id.edNombre).Text;
			String _Alias=FindViewById<EditText> (Resource.Id.edAlias).Text;
			String _Email=FindViewById<EditText> (Resource.Id.edEMail).Text;


			if ((String.IsNullOrWhiteSpace (_Nombre)) || (String.IsNullOrWhiteSpace (_Alias)) || (String.IsNullOrWhiteSpace (_Email))) 
			{
				Toast.MakeText (this,"Para agregar un nuevo contacto todos los campos deben ser concretados.",ToastLength.Long);
				return;
			}

			//Falta comprobar que la caducidad y la volatilidad estan correctamente indicadas.

			Contacto _NuevoContacto = new Contacto ();
			_NuevoContacto.Nombre = _Nombre;
			_NuevoContacto.Alias = _Alias;
			_NuevoContacto.EMail = _Email;
			_NuevoContacto.Caducidad = this._Caducidad;
			_NuevoContacto.Volatilidad = this._Volatilidad;

			if (_IsNew (_NuevoContacto)) 
			{
				try
				{
					Entorno.DB.Insert (_NuevoContacto);
					Toast.MakeText (this,"Contacto agregado correctamente",ToastLength.Long).Show ();

					//************CODIGO AÑADIDO POR ADRIAN*********************************/
					/**/Intent myIntent = new Intent (this, typeof(FormContacto));
					/**/SetResult (Result.Ok, myIntent);
					/**/Finish();
					//************CODIGO AÑADIDO POR ADRIAN*********************************/
				}
				catch(Exception ex)
				{
					Toast.MakeText (this,"Hubo un problema agregando al usuario. ERROR: "+ex.Message,ToastLength.Short).Show ();
				}
			}

		}

		public void Cancelar()
		{
			var iMain = new Intent (this, typeof(MainActivity));
			SetResult (Result.Canceled, iMain);
			Finish ();
		}

		private bool _CanExecuteAgregar()
		{
			return true;
		}

		private bool _IsNew(Contacto contacto)
		{
			return true;
		}

		private void spinnerCad_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			switch (e.Position) 
			{
				default:
				case 0:
					_Caducidad = new DateTime ().AddHours (1);
					break;
				case 1:
					_Caducidad = new DateTime ().AddDays (1);
					break;
				case 2:
					_Caducidad = new DateTime ().AddMonths (1);
					break;
				case 3:
					_Caducidad = new DateTime ().AddMonths (99);
					break;
			}
			Toast.MakeText(this,_ListaCaducidades [e.Position],ToastLength.Short).Show ();
		}

		private void spinnerVol_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			switch (e.Position) 
			{
				default:
				case 0:
					_Volatilidad = new DateTime ().AddSeconds (5);
					break;
				case 1:
					_Volatilidad = new DateTime ().AddSeconds (20);
					break;
				case 2:
					_Volatilidad = new DateTime ().AddMinutes (1);
					break;
				case 3:
					_Volatilidad = new DateTime ().AddHours (1);
					break;
				case 4:
					_Volatilidad = new DateTime ().AddHours (99);
					break;
			}
			Toast.MakeText(this,_ListaVolatilidades [e.Position],ToastLength.Short).Show ();
		}

		private void Mostrar()
		{
			var _IntListContacto = new Intent(this, typeof(ListaContactos));
			StartActivity(_IntListContacto);
		}
	}
}
