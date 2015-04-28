using System;
using SQLite;

namespace CBTablones
{
	[Table("PERMISOS")]
	public class Permiso
	{
		#region Constructor
		public Permiso()
		{			
		}
		#endregion

		#region Propiedades

		[PrimaryKey]
		public int TablonID{ get; set; }

		public int ContactID{ get; set; }

		[MaxLength(1)]
		public string Permiso{ get; set; }

		#endregion
	}
}
