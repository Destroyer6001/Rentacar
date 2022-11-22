using System.ComponentModel.DataAnnotations;

namespace Rentacar.Models
{
    public class VehiculoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        public string Modelo { get; set; }

        [Display(Name = "Numero De Plazas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int NumeroDePlazas { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Kilometraje { get; set; }

        [Display(Name = "Tipo de transmision")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(12, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        public string TipoDeCaja { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Color { get; set; }

        [Display(Name = "Valor de alquiler")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal ValorDeAlquiler { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(6, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        public string Placa { get; set; }

        public string NombreMarca { get; set; }
    }
}
