using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rentacar.Models
{
    public class VehiculoCreacionViewModel : VehiculoViewModel
    {
       public IEnumerable<SelectListItem> MarcaViewModel { get; set; }
    }
}
