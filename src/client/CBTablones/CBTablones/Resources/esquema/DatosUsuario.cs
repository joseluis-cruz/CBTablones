using System;
using SQLite;

namespace CBTablones
{
	[Table("DATOS_USUARIO")]
	public class DatosUsuario
	{
		#region Constructor
		public DatosUsuario ()
		{			
		}
		#endregion

		#region Propiedades

		[PrimaryKey]
		public int ID{ get; set; }

		[MaxLength(100)]
		public string EMail{ get; set; }

		[MaxLength(100)]
		public string Nombre{ get; set; }

		[MaxLength(50),Unique]
		public string Alias{ get; set; }

		[MaxLength(20)]
		public string Contrasenya{ get; set; }

		public DateTime Volatilidad{ get; set; } 

		public DateTime Caducidad{ get; set; } 

		[MaxLength(1)]
		public string Estado{ get; set; }

		[MaxLength(50)]
		public string Servidor{ get; set;}
		#endregion
	}
}

