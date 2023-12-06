using Dapper;
using Microsoft.Data.SqlClient;
using SendGen.Domain.SaborColonialDomains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Repository.SaborColonialRepositories
{
	// Repositório para interagir com a entidade clientes no banco de dados SaborColonial
	public class TransacionadoresRepository
    {
		// Método para obter a lista de clientes do banco de dados
		public List<Transacionadores> ObterClientes()
        {
			// String de conexão com o banco de dados SaborColonial
			string connectionString = "Server=localhost; Database=SaborColonial; Integrated Security=True;TrustServerCertificate=True;";

            // Inicializa a conexão
            using (var con = new SqlConnection(connectionString))
            {
                // Abre a conexão
                con.Open();

				// Utiliza Dapper para executar uma consulta SQL e mapear os resultados para uma lista de clientes
				List<Transacionadores> lista = con
                    .Query<Transacionadores>(@"SELECT TraCod, TraNom, TraCelular, TraDatNasc FROM [dbo].[TRANSACIONADORES]")
                    .ToList();

				// Retorna a lista de clientes
				return lista;
            }
        }
    }
}
