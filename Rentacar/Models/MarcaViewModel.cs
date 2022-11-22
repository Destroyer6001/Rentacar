using System.ComponentModel.DataAnnotations;

namespace Rentacar.Models
{
    public class MarcaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
    }
}
