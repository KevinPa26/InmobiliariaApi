using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Models
{
    public enum enEstadosInm
    {
        DISPONIBLE = 1,
        NODISPONIBLE = 2,
        ALQUILADO = 3,
        SUSPENDIDO = 4
    }

    public enum enEstadosUso
    {
        RESIDENCIAL = 1,
        COMERCIAL = 2,
    }

    public enum enEstadosTipo
    {
        CASA = 1,
        DEPARTAMENTO = 2,
        LOCAL = 3,
        DEPÓSITO = 4
    }

	public class Inmueble
	{
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "DUEÑO")]
        public int propietarioid { get; set; }
        [Display(Name = "DIRECCIÓN")]
		[Required]
		public string direccion { get; set; }
        [Display(Name = "USO")]
        [Required]
		public byte uso { get; set; }
        [Display(Name = "TIPO")]
        [Required]
		public byte tipo { get; set; }
        [Display(Name = "CANT AMBIENTE")]
		[Required]
		public int cant_ambiente { get; set; }
        [Display(Name = "PRECIO")]
        [Required]
		public double precio { get; set; }
        [Display(Name = "SUPERFICIE")]
		[Required]
		public int superficie { get; set; }
        [Display(Name = "IMAGEN")]
		[Required]
		public string imagen { get; set; }
        [Display(Name = "ESTADO")]
        [Required]
		public byte estado { get; set; }
        [Display(Name = "CREADO")]
        [Required]
		public System.DateTime creado { get; set; }
        [Display(Name = "MODIFICADO")]
        [Required]
		public System.DateTime modificado { get; set; }
        [NotMapped]//Para EF
        public string estadoNombre => estado > 0 ? ((enEstadosPro)estado).ToString() : "";

        public static IDictionary<int, string> ObtenerEstados()
        {
            SortedDictionary<int, string> estados = new SortedDictionary<int, string>();
            Type tipoEnumEst = typeof(enEstadosPro);
            foreach (var valor in Enum.GetValues(tipoEnumEst))
            {
                estados.Add((int)valor, Enum.GetName(tipoEnumEst, valor));
            }
            return estados;
        }

        public string estadoUso => uso > 0 ? ((enEstadosUso)uso).ToString() : "";

        public static IDictionary<int, string> ObtenerUsos()
        {
            SortedDictionary<int, string> estados = new SortedDictionary<int, string>();
            Type tipoEnumEst = typeof(enEstadosUso);
            foreach (var valor in Enum.GetValues(tipoEnumEst))
            {
                estados.Add((int)valor, Enum.GetName(tipoEnumEst, valor));
            }
            return estados;
        }

        public string estadoTipo => uso > 0 ? ((enEstadosTipo)uso).ToString() : "";

        public static IDictionary<int, string> ObtenerTipos()
        {
            SortedDictionary<int, string> estados = new SortedDictionary<int, string>();
            Type tipoEnumEst = typeof(enEstadosTipo);
            foreach (var valor in Enum.GetValues(tipoEnumEst))
            {
                estados.Add((int)valor, Enum.GetName(tipoEnumEst, valor));
            }
            return estados;
        }
        [ForeignKey(nameof(propietarioid))]
        public Propietario duenio { get; set; }
    }
}