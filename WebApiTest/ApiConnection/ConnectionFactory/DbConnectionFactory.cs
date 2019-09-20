using ApiConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConnection.ConnectionFactory
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {

        private readonly string cadCon;
        private readonly string provider;

        public DbConnectionFactory(string cadCon, string provider = "System.Data.SqlClient")
        {
            if (string.IsNullOrWhiteSpace(cadCon))
            {
                throw new ArgumentException("Cadena de Conexion Inválida", nameof(cadCon));
            }

            if (string.IsNullOrWhiteSpace(provider))
            {
                throw new ArgumentException("Proveedor de Datos Inválido", nameof(provider));
            }

            this.cadCon = cadCon;
            this.provider = provider;
        }

        /// <summary>
        /// Permite Crear una nueva conexion utilizando Cadena de Conexion por defecto
        /// Por Ahora Solo Manejaremos Conexiones Sql Server
        /// Pero La idea es soportar varios proveedores (Oracle, PSQL, Etc)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public IDbConnection Create()
        {
            var connection = new SqlConnection(cadCon);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Permite Crear una nueva conexion utilizando un ConnectionString Diferente
        /// Por Ahora Solo Manejaremos Conexiones Sql Server
        /// Pero La idea es soportar varios proveedores (Oracle, PSQL, Etc)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public IDbConnection Create(string connectionString, string provider = "System.Data.SqlClient")
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
