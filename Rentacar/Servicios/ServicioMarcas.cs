using Dapper;
using Microsoft.Data.SqlClient;
using Rentacar.Models;

namespace Rentacar.Servicios
{
    public class ServicioMarcas : IServicioMarcas
    {
        private readonly string ConnectionString;
        public ServicioMarcas(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(MarcaViewModel marca)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO MARCA (Descripcion)
                                                            VALUES (@DESCRIPCION);
                                                            SELECT SCOPE_IDENTITY();", marca);

            marca.Id = id;
        }

        public async Task<bool> Existe(string descripcion, int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM MARCA WHERE Descripcion = @Descripcion AND Id != @Id ", new {descripcion,id});

            return existe == 1;
        }

        public async Task<bool> ExisteCrear(string descripcion)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM MARCA WHERE Descripcion = @Descripcion", new {descripcion});

            return existe == 1;
        }


        public async Task<IEnumerable<MarcaViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<MarcaViewModel>(@"select Id, Descripcion from Marca");
        }

        public async Task<MarcaViewModel> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<MarcaViewModel>(@"select Id, Descripcion from Marca
                                                                            where Id = @Id", new {id});
        }

        public async Task Actualizar(MarcaViewModel marca)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"UPDATE MARCA SET DESCRIPCION = @DESCRIPCION WHERE ID=@ID", marca);
        }

        public async Task Borrar(int id)
        {
            var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"DELETE MARCA WHERE ID = @ID", new { id });
        }
    }
}
