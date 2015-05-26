
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
	[Activity (Label = "LoginActivity")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.LoginLo);
			FindViewById<Button> (Resource.Id.btnLoginAceptar).Click += delegate {
				string apodo = FindViewById<EditText> (Resource.Id.etLoginApodo).Text;
				string password = FindViewById <EditText> (Resource.Id.etLoginPassword).Text;
				try{
					if((apodo == "")&&(password == ""))
					{
						throw new Exception();
					}
					try{
						DatosUsuario usuario = Entorno.DB.Get<DatosUsuario>(1);
						if((usuario.Alias == apodo)&&(usuario.Contrasenya == password)){
							Toast.MakeText(this,"Login correcto",ToastLength.Long).Show();
						}
						else{
							Toast.MakeText(this,"El usuario no existe",ToastLength.Long).Show();
						}
					}
					catch{
						Toast.MakeText(this,"No hay ningún usuario",ToastLength.Long).Show();
					}


				}catch(Exception ex){
					Toast.MakeText(this,"Debe introducir nombre de usuario y contraseña",ToastLength.Long).Show();
				}
			};
		}
	}
}
