using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Models
{
	public class Contrato
	{
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "INMUEBLE")]
        public int inmuebleid { get; set; }
        [Display(Name = "INQUILINO")]
        public int inquilinoid { get; set; }
        [Display(Name = "FECHA INICIO")]
		[Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public System.DateTime fecha_inicio { get; set; }
        [Display(Name = "FECHA FIN")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public System.DateTime fecha_fin { get; set; }
        [Display(Name = "MONTO MENSUAL")]
        [Required]
		public double monto_mes { get; set; }
        [Display(Name = "NOMBRE DEL GARANTE")]
        [Required]
		public string garante_nombre { get; set; }
        [Display(Name = "APELLIDO DEL GARANTE")]
        [Required]
		public string garante_apellido { get; set; }
        [Display(Name = "DNI DEL GARANTE")]
        [Required]
		public string garante_dni { get; set; }
        [Display(Name = "TEL DEL GARANTE")]
        [Required]
		public string garante_tel { get; set; }
        [Display(Name = "CREADO")]
        [Required]
		public System.DateTime creado { get; set; }
        [Display(Name = "MODIFICADO")]
        [Required]
		public System.DateTime modificado { get; set; }
        [ForeignKey(nameof(inmuebleid))]
        public Inmueble inmueble { get; set; }
        [ForeignKey(nameof(inquilinoid))]
        public Inquilino inquilino { get; set; }
    }
}