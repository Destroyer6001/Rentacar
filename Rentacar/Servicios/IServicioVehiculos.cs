using Rentacar.Models;

namespace Rentacar.Servicios
{
    public interface IServicioVehiculos
    {
        Task Actualizar(VehiculoCreacionViewModel vehiculo);
        Task Borrar(int id);
        Task Crear(VehiculoViewModel vehiculo);
        Task<bool> Existe(string placa, int id);
        Task<bool> ExisteCrear(string placa);
        Task<IEnumerable<VehiculoViewModel>> Obtener();
        Task<VehiculoViewModel> ObtenerPorId(int id);
    }
}
