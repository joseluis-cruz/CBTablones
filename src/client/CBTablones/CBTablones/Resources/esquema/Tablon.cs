using System;
using SQLite;

namespace CBTablones
{
	[Table("TABLONES")]
	public class Tablon
	{
		#region Constructor
		public Tablon ()
		{			
		}
		#endregion

		#region Propiedades

		[PrimaryKey, AutoIncrement]
		public int Id{ get; set; }

		[MaxLength(100)]
		public string Nombre{ get; set; }

		public DateTime Caducidad{ get; set; } 

		public DateTime Volatilidad{ get; set; }

		[MaxLength(1)]
		public string Estado{ get; set; }

		#endregion
	}
}

