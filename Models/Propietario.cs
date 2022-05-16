using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaApi.Models
{
    public enum enEstadosPro
    {
        DISPONIBLE = 1,
        NODISPONIBLE = 2,
        SUSPENDIDO = 3
    }
    public class Propietario
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "DNI")]
		[Required]
        public string dni { get; set; }
        [Display(Name = "NOMBRE")]
        [Required]
		public string nombre { get; set; }
        [Display(Name = "APELLIDO")]
		[Required]
		public string apellido { get; set; }
        [Display(Name = "TEL")]
		public string tel { get; set; }
        [Display(Name = "EMAIL")]
		[Required, EmailAddress]
		public string email { get; set; }
        [Display(Name = "DIRECCIÓN")]
        [Required]
        public string direccion { get; set; }
        [Display(Name = "ESTADO")]
        public byte estado { get; set; }
        [Display(Name = "CREADO")]
        [DataType(DataType.DateTime)]
        public System.DateTime creado { get; set; }
        [Display(Name = "MODIFICADO")]
        [DataType(DataType.DateTime)]
        public System.DateTime modificado { get; set;}

        [NotMapped]//Para EF
        public string estadoNombre => estado > 0 ? ((enEstadosPro)estado).ToString() : "";

        public static IDictionary<int, string> ObtenerEstados()
        {
            SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
            Type tipoEnumEst = typeof(enEstadosPro);
            foreach (var valor in Enum.GetValues(tipoEnumEst))
            {
                roles.Add((int)valor, Enum.GetName(tipoEnumEst, valor));
            }
            return roles;
        }

        public override string ToString()
        {
            return $"{nombre} { apellido}";
        }

	}
}