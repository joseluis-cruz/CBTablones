using System;
using SQLite;

namespace CBTablones
{
	[Table("CONTACTOS")]
	public class Contacto
	{
		#region Constructor
		public Contacto ()
		{			
		}
		#endregion

		#region Propiedades

		[PrimaryKey, AutoIncrement]
		public int ID{ get; set; }

		[MaxLength(100)]
		public string EMail{ get; set; }

		[MaxLength(100)]
		public string Nombre{ get; set; }

		[MaxLength(50),Unique]
		public string Alias{ get; set; }

		public DateTime Volatilidad{ get; set; } 

		public DateTime Caducidad{ get; set; } 

		[MaxLength(1)]
		public string Estado{ get; set; }

		#endregion
	}
}

