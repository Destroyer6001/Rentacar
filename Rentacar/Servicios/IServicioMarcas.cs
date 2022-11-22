using Rentacar.Models;

namespace Rentacar.Servicios
{
    public interface IServicioMarcas
    {
        Task Actualizar(MarcaViewModel marca);
        Task Borrar(int id);
        Task Crear(MarcaViewModel marca);
        Task<bool> Existe(string descripcion, int id);
        Task<bool> ExisteCrear(string descripcion);
        Task<IEnumerable<MarcaViewModel>> Obtener();
        Task<MarcaViewModel> ObtenerPorId(int id);
    }
}
