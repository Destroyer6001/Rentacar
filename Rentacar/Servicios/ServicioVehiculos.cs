using Dapper;
using Microsoft.Data.SqlClient;
using Rentacar.Models;
using System.Collections;
using System.Xml.Linq;

namespace Rentacar.Servicios
{
    public class ServicioVehiculos : IServicioVehiculos
    {
        private readonly string ConnectionString;
        public ServicioVehiculos(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(VehiculoViewModel vehiculo)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO VEHICULO (Modelo,NumeroDePlazas,Kilometraje,
                                                            TipoDeCaJa,Color,ValorDeAlquiler,IdMarca,Placa) VALUES
                                                            (@MODELO, @NUMERODEPLAZAS, @kiLOMETRAJE, @TIPODECAJA, 
                                                            @COLOR, @VALORDEALQUILER, @IDMARCA, @PLACA);
                                                            SELECT SCOPE_IDENTITY();",vehiculo);

             
           vehiculo.Id = id;
        }

        public async Task<bool> ExisteCrear(string placa)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM VEHICULO
                                                                        WHERE Placa = @PLACA", new {placa});

            return existe == 1; 
        }

        public async Task<bool> Existe(string placa, int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM VEHICULO
                                                                        WHERE Placa = @PLACA AND Id != @Id", new { placa,id});

            return existe == 1;
        }

        public async Task<IEnumerable<VehiculoViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<VehiculoViewModel>(@"SELECT VE.Id,Modelo,NumeroDePlazas,Kilometraje,TipoDeCaJa,
                                                                    Color,ValorDeAlquiler,Placa,IdMarca,MA.DESCRIPCION AS NOMBREMARCA  
                                                                    FROM VEHICULO VE
                                                                    INNER JOIN MARCA MA
                                                                    ON MA.Id = VE.IdMarca");
        }

        public async Task<VehiculoViewModel> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<VehiculoViewModel>(@"SELECT VE.Id,Modelo,NumeroDePlazas,Kilometraje,
                                                                                TipoDeCaJa,Color,ValorDeAlquiler,Placa,IdMarca
                                                                                FROM VEHICULO VE
                                                                                INNER JOIN MARCA MA
                                                                                ON MA.Id = VE.IdMarca
                                                                                WHERE VE.ID = @ID", new {id});
        }

        public async Task Actualizar(VehiculoCreacionViewModel vehiculo)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"UPDATE VEHICULO SET
                                            Modelo = @MODELO,
                                            NumeroDePlazas = @NUMERODEPLAZAS,
                                            Kilometraje = @KILOMETRAJE,
                                            TipoDeCaJa = @TIPODECAJA,
                                            Color = @COLOR,
                                            ValorDeAlquiler = @VALORDEALQUILER,
                                            IdMarca = @IDMARCA,
                                            Placa = @PLACA
                                            WHERE Id = @ID", vehiculo);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"DELETE VEHICULO WHERE Id = @ID", new {id});
        }

    }
}
    