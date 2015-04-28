using System;
using SQLite;

namespace CBTablones
{
	public static class Entorno
	{
		/**
		 * Objeto del ORM que gobierna la conexión a la base de datos SQLite
		 */
		public static SQLiteConnection DB { get; set; }

		/**
		 * Inicializa la conexión con la base de datos, haciéndola residir físicamente en la ruta
		 * destinada por el sistema a los archivos personales de la aplicación, con el nombre "cbtablones.s3db"
		 */
		public static void Init()
		{
			string dbPath = System.IO.Path.Combine (
				System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal)
				, "cbtablones.s3db");				
			DB = new SQLiteConnection (dbPath);
		}

	}
}

