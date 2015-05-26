
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Provider;
using Android.Content.PM;
using Android.Net;
using Java.IO;

namespace CBTablones
{
	[Activity (Label = "FormContacto")]			
	public class FormContacto : Activity
	{
		private Button _BtnAceptar;
		private Button _BtnCancelar;
		private Button _BtnTomarFoto;
		private Spinner _SpCaducidad;
		private Spinner _SpVolatilidad;
		private String[] _ListaCaducidades =   {"1 Hora","1 Dia","1 Mes","Nunca"};
		private String[] _ListaVolatilidades = {"5 Segundos","20 Segundos","1 Minuto","1 Hora","Nunca"};
		private DateTime _Caducidad;
		private DateTime _Volatilidad;
		private ImageView _ImageView;

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

			_BtnAceptar.Click += delegate {
				Agregar ();
			};
			_BtnCancelar.Click += delegate {
				Cancelar ();
			};				

			_SpCaducidad = FindViewById<Spinner> (Resource.Id.spCaducidad);
			_SpCaducidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerCad_ItemSelected);
			_SpCaducidad.Adapter = new ArrayAdapter(this,Resource.Layout.TextViewItemLo,this._ListaCaducidades);

			_SpVolatilidad = FindViewById<Spinner> (Resource.Id.spVolatilidad);
			_SpVolatilidad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinnerVol_ItemSelected);
			_SpVolatilidad.Adapter = new ArrayAdapter (this,Resource.Layout.TextViewItemLo,this._ListaVolatilidades);

			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				_BtnTomarFoto = FindViewById<Button>(Resource.Id.btnTomarFoto);
				_ImageView = FindViewById<ImageView>(Resource.Id.imageView1);
				if (App.bitmap != null) 
				{
					_ImageView.SetImageBitmap (App.bitmap);
					App.bitmap = null;
				}

				_BtnTomarFoto.Click += delegate {
					TomarFoto();
				};
			}
		}

		public void Agregar()
		{
			String _Nombre=FindViewById<EditText> (Resource.Id.edNombre).Text;
			String _Alias=FindViewById<EditText> (Resource.Id.edAlias).Text;
			String _Email=FindViewById<EditText> (Resource.Id.edEMail).Text;


			if ((String.IsNullOrWhiteSpace (_Nombre)) || (String.IsNullOrWhiteSpace (_Alias)) || (String.IsNullOrWhiteSpace (_Email))) 
			{
				Toast.MakeText (this,"Para agregar un nuevo contacto todos los campos deben ser concretados.",ToastLength.Short);
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
					if (App._file != null)
					{
						Contacto tmpContacto = Entorno.DB.Table<Contacto> ().Where (t => t.Alias.Equals(_Alias)).First();
						Java.IO.File tmpFile = new Java.IO.File(App._dir, String.Format("photo_{0}.jpg", tmpContacto.ID));
						App._fileCrop.RenameTo(tmpFile);
						App._file.Delete ();
					}

					Toast.MakeText (this,"Contacto agregado correctamente",ToastLength.Short).Show ();

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
			App._file.Delete ();
			App._fileCrop.Delete ();
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
		}
			

		private void TomarFoto ()
		{
			
			var intent = new Intent(MediaStore.ActionImageCapture);
			App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", 0));
			App._fileCrop = new Java.IO.File(App._dir, String.Format("photo_{0}_crop.jpg",0));
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
			StartActivityForResult(intent, 2);

		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CTablones");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
				
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			//base.OnActivityResult(requestCode, resultCode, data);

			if ((resultCode==Result.Ok) && (requestCode == 2))
			{
				// make it available in the gallery
				Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				var contentUri = Android.Net.Uri.FromFile(App._file);
				mediaScanIntent.SetData(contentUri);
				SendBroadcast(mediaScanIntent);

				//call the standard crop action intent (the user device may not support it)
				Intent cropIntent = new Intent("com.android.camera.action.CROP"); 
				//indicate image type and Uri
				cropIntent.SetDataAndType(contentUri, "image/*");
				cropIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._fileCrop));
				//set crop properties
				cropIntent.PutExtra("crop", "true");
				//indicate aspect of desired crop
				cropIntent.PutExtra("aspectX", 1);
				cropIntent.PutExtra("aspectY", 1);
				//indicate output X and Y
				cropIntent.PutExtra("outputX", 512);
				cropIntent.PutExtra("outputY", 512);
				//retrieve data on return
				cropIntent.PutExtra("return-data", true);
				//start the activity - we handle returning in onActivityResult
				StartActivityForResult(cropIntent, 3);
			}
			else if (requestCode == 3)
			{
				Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				var contentUri = Android.Net.Uri.FromFile(App._fileCrop);
				mediaScanIntent.SetData(contentUri);
				SendBroadcast(mediaScanIntent);

				// display in ImageView. We will resize the bitmap to fit the display
				// Loading the full sized image will consume to much memory 
				// and cause the application to crash.
				int height = 512;
				int width = 512;

				App.bitmap = App._fileCrop.Path.LoadAndResizeBitmap (width, height);
				if (App.bitmap != null) 
				{
					_ImageView.SetImageBitmap (App.bitmap);
					App.bitmap = null;
				}
			}

		}
	}

	public static class App
	{
		public static Java.IO.File _file;
		public static Java.IO.File _fileCrop;
		public static Java.IO.File _dir;     
		public static Bitmap bitmap;
	}
}
