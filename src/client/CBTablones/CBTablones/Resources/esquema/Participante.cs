using System;
using SQLite;

namespace CBTablones
{
	// Tabla con la relación "tablón-contactos (participantes)"
	[Table("PARTICIPANTE")]
	public class Participante
	{
		[PrimaryKey]
		public int IdTablon{ get; set; }

		[PrimaryKey]
		public int IdContact{ get; set; }

		[MaxLength(1)]
		public string Permiso{ get; set; }

		[MaxLength(30)]
		public string NombreContacto{ get; set; }
	}
}
