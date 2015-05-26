using System;
using System.Runtime.Serialization;

namespace TestJson
{

	[DataContract]
	public class CDireccion
	{
		[DataMember]
		public string Tipo_Via;

		[DataMember]
		public string Nombre_Via;

		[DataMember]
		public int Numero;

		[DataMember]
		public string Codigo_Postal;

		[DataMember]
		public string Localidad;
	}

	[DataContract]
	public class Persona
	{
		[DataMember]
		public string Nombre;

		[DataMember]
		public string Apellidos;

		[DataMember]
		public CDireccion Direccion;

		[DataMember]
		public string[] Emails;

		public Persona ()
		{
			Direccion = new CDireccion ();
		}
	}
}

