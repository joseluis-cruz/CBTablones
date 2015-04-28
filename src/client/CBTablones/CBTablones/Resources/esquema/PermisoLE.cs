using System;
using SQLite;

namespace CBTablones
{
	[Table("PERMISOSLE")]
	public class PermisoLE
	{
		#region Constructor
		public PermisoLE()
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
