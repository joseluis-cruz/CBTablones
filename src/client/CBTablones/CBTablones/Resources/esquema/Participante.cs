using System;
using SQLite;

namespace CBTablones
{
	// Tabla con la relación "tablón-contactos (participantes)"
	[Table("PARTICIPANTES")]
	public class Participante
	{
		#region Constructor
		public Participante ()
		{
		}
		#endregion

		#region Propiedades

		// proporcionado por la llamada de "añadir participantes" (Autor: Jonathan)
		[PrimaryKey]
		public int IDTablonP { get; set; }

		// Lo obtendremos de la lista de contactos
		public int IDP { get; set; }

		[MaxLength(100)]
		public string NombreP{ get; set; }

		[MaxLength(50),Unique]
		public string AliasP { get; set; }

		[MaxLength(1)]
		public string PermisoLEP { get; set; }

		#endregion
	}
}
