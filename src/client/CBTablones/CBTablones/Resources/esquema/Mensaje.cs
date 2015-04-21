using System;

using SQLite;

namespace CBTablones
{
	[Table("MENSAJES")]
	public class Mensaje
	{

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Contenido { get; set; }

		public int ContacOrigen { get; set; }

		public int ContacDest { get; set; }

		[AutoIncrement]
		public int IdTablon { get; set; }

		public DateTime FHoraCreacion { get; set; }

		public DateTime FHoraServidor { get; set; }

		public DateTime FHoraRecibe { get; set; }

		public DateTime FHoraLee { get; set; }


		public Mensaje ()
		{

		}
	}
}

