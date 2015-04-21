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

		[PrimaryKey, AutoIncrementAttribute]
		public int IDContacto{ get; set; }

		[MaxLength(60)]
		public string EMail{ get; set; }

		[MaxLength(60)]
		public string Nombre{ get; set; }

		[MaxLength(60)]
		public string Alias{ get; set; }

		public DateTime DefaultVolat{ get; set; } 

		[MaxLength(1)]
		public string Estado{ get; set; }

		#endregion
	}
}

