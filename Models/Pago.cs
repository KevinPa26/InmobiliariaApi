using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Models
{
	public class Pago
	{
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "CONTRATO")]
        public int contratoid { get; set; }
        [Display(Name = "FECHA")]
		[Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public System.DateTime fecha { get; set; }
        [Display(Name = "IMPORTE")]
        [Required]
		public double importe { get; set; }
        [Display(Name = "N° DE PAGO")]
        [Required]
		public int num_pago { get; set; }
        [Display(Name = "CREADO")]
        [Required]
		public System.DateTime creado { get; set; }
        [Display(Name = "MODIFICADO")]
        [Required]
		public System.DateTime modificado { get; set; }
        [ForeignKey(nameof(contratoid))]
        public Contrato contrato { get; set; }
    }
}